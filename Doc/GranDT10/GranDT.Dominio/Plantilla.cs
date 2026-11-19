namespace GranDT.Dominio;

public sealed class Plantilla
{
    public const decimal PresupuestoMaximo = 100_000_000m;
    public const int MaximoFutbolistas = 20;
    private static readonly IReadOnlyDictionary<TipoFutbolista, int> Formacion = new Dictionary<TipoFutbolista, int>
    {
        [TipoFutbolista.Arquero] = 1,
        [TipoFutbolista.Defensor] = 4,
        [TipoFutbolista.Mediocampista] = 4,
        [TipoFutbolista.Delantero] = 2
    };

    public long IdPlantilla { get; }
    public string NombreP { get; }
    public decimal Presupuesto { get; }
    public int CantFutbolista { get; }
    public long IdUsuario { get; }
    public IReadOnlyList<PlantillaFutbolista> Detalles { get; }

    public Plantilla(long idPlantilla, string nombreP, decimal presupuesto, int cantFutbolista, long idUsuario, IReadOnlyList<PlantillaFutbolista>? detalles = null)
    {
        if (string.IsNullOrWhiteSpace(nombreP)) throw new ReglaNegocioException("El nombre de la plantilla es obligatorio.");
        if (presupuesto is < 0m or > PresupuestoMaximo) throw new ReglaNegocioException("El presupuesto debe estar entre 0 y 100.000.000.");
        if (cantFutbolista is < 1 or > MaximoFutbolistas) throw new ReglaNegocioException("La cantidad debe estar entre 1 y 20 futbolistas.");
        if (idUsuario <= 0) throw new ReglaNegocioException("El usuario debe ser válido.");
        IdPlantilla = idPlantilla;
        NombreP = nombreP.Trim();
        Presupuesto = presupuesto;
        CantFutbolista = cantFutbolista;
        IdUsuario = idUsuario;
        Detalles = detalles ?? [];
    }

    public decimal CostoActual => Detalles.Sum(d => d.Futbolista.Cotizacion);
    public bool PresupuestoValido => CostoActual <= Presupuesto;
    public decimal PuntajeFecha(int nroFecha) => Detalles
        .Where(d => d.FutbolistaTitular)
        .Sum(d => d.Futbolista.PuntajeEnFecha(nroFecha));

    public IReadOnlyList<string> Validar()
    {
        var errores = new List<string>();
        if (Detalles.Count > CantFutbolista || Detalles.Count > MaximoFutbolistas)
            errores.Add("La plantilla supera la cantidad máxima de futbolistas.");
        if (Detalles.GroupBy(d => d.Futbolista.IdFutbolista).Any(g => g.Count() > 1))
            errores.Add("No se puede repetir un futbolista en la plantilla.");
        if (!PresupuestoValido) errores.Add("El costo de los futbolistas supera el presupuesto.");

        var titulares = Detalles.Where(d => d.FutbolistaTitular).ToList();
        foreach (var (tipo, cantidad) in Formacion)
        {
            var actual = titulares.Count(d => d.Futbolista.Tipo == tipo);
            if (actual != cantidad) errores.Add($"La formación requiere {cantidad} {NombreTipo(tipo)} titular(es); hay {actual}.");
        }
        if (titulares.Count != 11) errores.Add("La formación titular debe tener exactamente 11 futbolistas.");
        return errores;
    }

    private static string NombreTipo(TipoFutbolista tipo) => tipo switch
    {
        TipoFutbolista.Arquero => "arquero",
        TipoFutbolista.Defensor => "defensores",
        TipoFutbolista.Mediocampista => "mediocampistas",
        _ => "delanteros"
    };
}