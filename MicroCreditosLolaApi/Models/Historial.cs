using System;
using System.Collections.Generic;

namespace MicroCreditosLolaApi.Models;

public partial class Historial
{
    public int Idmonto { get; set; }

    public decimal? Monto { get; set; }

    public int? Periodopago { get; set; }

    public DateTime? Fechadepago { get; set; }

    public string? Status { get; set; }

    public int? Idcliente { get; set; }

    public virtual Cliente? IdclienteNavigation { get; set; }
}
