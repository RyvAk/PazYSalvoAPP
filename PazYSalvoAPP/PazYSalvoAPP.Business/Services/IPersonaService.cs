using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public interface IPersonaService
    {
        Task<bool> Insertar(Persona model);
        Task<bool> Actualizar(Persona model);
        Task<Persona> Leer(int id); // ?
        Task<IQueryable<Persona>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);

    }
    }