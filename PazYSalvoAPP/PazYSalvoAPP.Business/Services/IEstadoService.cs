using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public interface IEstadoService
    {
        Task<bool> Insertar(Estado model);
        Task<bool> Actualizar(Estado model);
        Task<Estado> Leer(int id); // ?
        Task<IQueryable<Estado>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);

    }
}
