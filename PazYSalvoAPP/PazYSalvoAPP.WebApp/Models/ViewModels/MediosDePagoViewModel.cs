﻿using PazYSalvoAPP.Models;

namespace PazYSalvoAPP.WebApp.Models.ViewModels
{
    public class MediosDePagoViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        

        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}