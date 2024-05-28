using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public interface IPagoService
    {
        Task<bool> Insertar(Pago model);
        Task<bool> Actualizar(Pago model);
        Task<Pago> Leer(int id); // ?
        Task<IQueryable<Pago>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);

    }
}
