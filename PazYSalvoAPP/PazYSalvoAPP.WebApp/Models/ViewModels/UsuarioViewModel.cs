﻿using PazYSalvoAPP.Models;

namespace PazYSalvoAPP.WebApp.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        public int? PersonaId { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public DateTime? FechaDeCreacion { get; set; }

        public virtual Persona? Persona { get; set; }
    }
}