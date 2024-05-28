using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public interface IMediosDePagoService
    {
        Task<bool> Insertar(MediosDePago model);
        Task<bool> Actualizar(MediosDePago model);
        Task<MediosDePago> Leer(int id); // ?
        Task<IQueryable<MediosDePago>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);

    }
}
