﻿using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public interface IUsuarioService
    {
        Task<bool> Insertar(Usuario model);
        Task<bool> Actualizar(Usuario model);
        Task<Usuario> Leer(int id); // ?
        Task<IQueryable<Usuario>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);
    }
}