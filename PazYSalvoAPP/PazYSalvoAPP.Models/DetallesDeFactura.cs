using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Models;

public partial class DetallesDeFactura
{
    public int Id { get; set; }

    public int? FacturaId { get; set; }

    public int? PagoId { get; set; }

    public DateTime? FechaDeCreacion { get; set; }

    public virtual Factura? Factura { get; set; }

    public virtual Pago? Pago { get; set; }
}
