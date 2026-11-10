using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoPlantilla : RepoDapper, IRepoPlantilla
{
    private const string SelectSql = @"
        SELECT idPlantilla, nombreP, cantMaxMonto, cantMaxFutbolista, idUsuario
        FROM Plantilla";

    public RepoPlantilla(IDbConnection conexion) : base(conexion)
    {
    }

    public List<Plantilla> ObtenerTodos()
    {
        var plantillas = Conexion.Query<Plantilla>($"{SelectSql} ORDER BY idPlantilla").AsList();
        CargarDetalles(plantillas);
        return plantillas;
    }

    public Plantilla? ObtenerPorId(ushort id)
    {
        var plantilla = Conexion.QuerySingleOrDefault<Plantilla>(
            $"{SelectSql} WHERE idPlantilla = @Id LIMIT 1",
            new { Id = id });

        if (plantilla is null)
            return null;

        CargarDetalles(new List<Plantilla> { plantilla });
        return plantilla;
    }


    private void CargarDetalles(IList<Plantilla> plantillas)
    {
        if (plantillas.Count == 0)
            return;

        foreach (var plantilla in plantillas)
            plantilla.detalles = new List<PlantillaFutbolista>();

        var idsPlantillas = plantillas.Select(p => p.idPlantilla).Distinct().ToArray();
        var detalles = Conexion.Query<PlantillaFutbolista>(@"
            SELECT futbolistaTitular, idPlantilla, idFutbolista
            FROM Futbolista_Plantilla
            WHERE idPlantilla IN @IdsPlantillas
            ORDER BY idPlantilla, idFutbolista", new { IdsPlantillas = idsPlantillas }).AsList();

        if (detalles.Count == 0)
            return;

        var idsJugadores = detalles.Select(d => d.idFutbolista).Distinct().ToArray();
        var jugadores = Conexion.Query<Futbolista>(@"
                SELECT idFutbolista, nombre, apellido, apodo, fechaNac, cotizacion,
                       idEquipo, idTipoFutbolista
                FROM Futbolista
                WHERE idFutbolista IN @IdsJugadores", new { IdsJugadores = idsJugadores })
            .ToDictionary(j => j.idFutbolista);

        var puntuaciones = Conexion.Query<Puntuacion>(@"
                SELECT idPuntuacion, idFutbolista, puntuacion, nroFecha AS numFech
                FROM Puntuacion
                WHERE idFutbolista IN @IdsJugadores", new { IdsJugadores = idsJugadores })
            .ToLookup(p => p.idFutbolista);

        foreach (var jugador in jugadores.Values)
            jugador.puntuaciones = puntuaciones[jugador.idFutbolista].ToList();

        foreach (var detalle in detalles)
        {
            detalle.futbolista = jugadores.TryGetValue(detalle.idFutbolista, out var jugador) ? jugador : null;
        }

        var detallesPorPlantilla = detalles.ToLookup(d => d.idPlantilla);
        foreach (var plantilla in plantillas)
            plantilla.detalles = detallesPorPlantilla[plantilla.idPlantilla].ToList();
    }

    public Plantilla Agregar(Plantilla plantilla)
    {
        ArgumentNullException.ThrowIfNull(plantilla);
        var parametros = new DynamicParameters();
        parametros.Add("unidPlantilla", dbType: DbType.Byte, direction: ParameterDirection.Output);
        parametros.Add("unnombreP", plantilla.nombreP);
        parametros.Add("uncantMaxMonto", plantilla.cantMaxMonto);
        parametros.Add("uncantMaxFutbolista", plantilla.cantMaxFutbolista);
        parametros.Add("unidUsuario", plantilla.idUsuario);

        Conexion.Execute("AltaPlantilla", parametros, commandType: CommandType.StoredProcedure);
        plantilla.idPlantilla = parametros.Get<byte>("unidPlantilla");

        return plantilla;
    }

    public bool Eliminar(ushort id) =>
        Conexion.Execute("DELETE FROM Plantilla WHERE idPlantilla = @Id", new { Id = id }) > 0;
}