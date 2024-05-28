using PazYSalvoAPP.Models;
using System.ComponentModel.DataAnnotations;

namespace PazYSalvoAPP.WebApp.Models.ViewModels
{
    public class EstadoViewModel
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }


    }
}
