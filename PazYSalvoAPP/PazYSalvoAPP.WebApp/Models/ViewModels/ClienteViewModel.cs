using Microsoft.EntityFrameworkCore;
using PazYSalvoAPP.Models;
using System.ComponentModel.DataAnnotations;

namespace PazYSalvoAPP.WebApp.Models.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        public int? PersonaId { get; set; }

        public int? RolId { get; set; }

        public DateTime? FechaDeCreacion { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

        public virtual Persona? Persona { get; set; }

        public virtual Role? Rol { get; set; }
    }
}