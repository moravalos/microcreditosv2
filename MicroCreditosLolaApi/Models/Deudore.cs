using System;
using System.Collections.Generic;

namespace MicroCreditosLolaApi.Models;

public partial class Deudore
{
    public int Iddeudor { get; set; }

    public int? Idcliente { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public decimal? Montodebe { get; set; }

    public decimal? Montopagado { get; set; }

    public decimal? Montofinal { get; set; }

    public virtual Cliente? IdclienteNavigation { get; set; }
}
