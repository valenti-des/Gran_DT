using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoPlantilla : RepoDapper, IPlantillaRepository
{
    private const string SelectSql = @"
        SELECT idPlantilla, nombreP, cantMaxMonto, cantMaxFutbolista, idUsuario
        FROM Plantilla";

    public RepoPlantilla(IDbConnection conexion) : base(conexion)
    {
    }

    public List<Plantilla> ObtenerTodos()
    {
        var plantillas = Conexion.Query<Plantilla>(SelectSql).AsList();
        CargarDetalles(plantillas);
        return plantillas;
    }

    public Plantilla? ObtenerPorId(byte idPlantilla)
    {
        var plantilla = Conexion.QuerySingleOrDefault<Plantilla>(
            $"{SelectSql} WHERE idPlantilla = @IdPlantilla LIMIT 1",
            new { IdPlantilla = idPlantilla });

        if (plantilla is null)
            return null;

        CargarDetalles(new List<Plantilla> { plantilla });
        return plantilla;
    }

    private void CargarDetalles(IList<Plantilla> plantillas)
    {
        if (plantillas.Count == 0)
            return;

        var detalles = Conexion.Query<PlantillaFutbolista>(@"
            SELECT idFutbolistaPlantilla, futbolistaTitular, validacionP, validacionR,
                   idPlantilla, idFutbolista
            FROM Futbolista_Plantilla").ToList();

        foreach (var plantilla in plantillas)
            plantilla.detalles = new List<PlantillaFutbolista>();

        if (detalles.Count == 0)
            return;

        var idsJugadores = detalles.Select(d => d.idFutbolista).Distinct().ToArray();
        var jugadores = idsJugadores.Length == 0
            ? new Dictionary<ushort, Futbolista>()
            : Conexion.Query<Futbolista>(@"
                SELECT idFutbolista, nombre, apellido, apodo, fechaNac, cotizacion,
                       idEquipo, idTipoFutbolista
                FROM Futbolista").AsList()
                .Where(j => idsJugadores.Contains(j.idFutbolista))
                .ToDictionary(j => j.idFutbolista);

        var puntuaciones = Conexion.Query<Puntuacion>(@"
                SELECT idPuntuacion, idFutbolista, puntuacion, cantFech
                FROM Puntuacion").AsList();

        foreach (var jugador in jugadores.Values)
        {
            jugador.puntuaciones = puntuaciones
                .Where(p => p.idFutbolista == jugador.idFutbolista)
                .ToList();
        }

        foreach (var plantilla in plantillas)
        {
            plantilla.detalles = detalles
                .Where(d => d.idPlantilla == plantilla.idPlantilla)
                .Select(d =>
                {
                    d.futbolista = jugadores.TryGetValue(d.idFutbolista, out var jugador) ? jugador : null;
                    return d;
                })
                .ToList();
        }
    }

    public Plantilla Agregar(Plantilla plantilla)
    {
        ArgumentNullException.ThrowIfNull(plantilla);

        if (string.IsNullOrWhiteSpace(plantilla.nombreP))
            throw new ArgumentException("El nombre de la plantilla es obligatorio.", nameof(plantilla));

        plantilla.nombreP = plantilla.nombreP.Trim();
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

    public bool Eliminar(byte idPlantilla) =>
        Conexion.Execute("DELETE FROM Plantilla WHERE idPlantilla = @IdPlantilla", new { IdPlantilla = idPlantilla }) > 0;
}