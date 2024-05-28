using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public interface IServicioService
    {
        Task<bool> Insertar(Servicio model);
        Task<bool> Actualizar(Servicio model);
        Task<Servicio> Leer(int id); // ?
        Task<IQueryable<Servicio>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);
    }
}
