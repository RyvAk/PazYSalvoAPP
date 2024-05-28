﻿using PazYSalvoAPP.Models;

namespace PazYSalvoAPP.WebApp.Models.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaDeCreacion { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}