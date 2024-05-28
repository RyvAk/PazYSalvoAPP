﻿using PazYSalvoAPP.Models;

namespace PazYSalvoAPP.WebApp.Models.ViewModels
{
    public class PersonaViewModel
    {
        public int Id { get; set; }

        public string Nombres { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public string DocumentoIdentificacion { get; set; } = null!;

       

        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}