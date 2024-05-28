using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public interface IClienteService
    {
        Task<bool> Insertar(Cliente model);
        Task<bool> Actualizar(Cliente model);
        Task<Cliente> Leer(int id); // ?
        Task<IQueryable<Cliente>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);

    }
}