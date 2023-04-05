using System;
using System.Collections.Generic;

namespace MicroCreditosLolaApi.Models;

public partial class Cliente
{
    public int Idcliente { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidop { get; set; }

    public string? Apellidom { get; set; }

    public decimal? Cantidadp { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public DateTime? Fechap { get; set; }

    public short? Diacobro { get; set; }

    public int? Mesesprestamo { get; set; }

    public short? Intereses { get; set; }

    public decimal? Montodebe { get; set; }

    public int Montopagado { get; set; }

    public decimal? Montofinal { get; set; }

    public virtual ICollection<Deudore> Deudores { get; } = new List<Deudore>();

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();
}
