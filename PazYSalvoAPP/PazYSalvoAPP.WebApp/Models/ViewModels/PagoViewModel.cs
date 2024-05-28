﻿using PazYSalvoAPP.Models;

namespace PazYSalvoAPP.WebApp.Models.ViewModels
{
    public class PagoViewModel
    {
        public int Id { get; set; }

        public decimal? MontoDePago { get; set; }

        public int? FacturaId { get; set; }

        public bool? Activo { get; set; }

        

        public virtual ICollection<DetallesDeFactura> DetallesDeFacturas { get; set; } = new List<DetallesDeFactura>();

        public virtual Factura? Factura { get; set; }
    }
}