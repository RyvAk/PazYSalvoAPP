using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public interface IRoleService
    {
        Task<bool> Insertar(Role model);
        Task<bool> Actualizar(Role model);
        Task<Role> Leer(int id); // ?
        Task<IQueryable<Role>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);

    }
}
