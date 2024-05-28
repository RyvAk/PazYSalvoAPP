﻿using Microsoft.EntityFrameworkCore;
using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly PazSalvoContext _context;
        public UsuarioService(PazSalvoContext context)
        {
            _context = context;
        }

        public async Task<bool> Actualizar(Usuario model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            int usuarioId = model.Id;

            if (usuarioId == 0 || usuarioId == null) return result;

            try
            {
                Usuario usuario = await Leer(usuarioId);


                usuario.PersonaId = model.PersonaId;
                usuario.NombreUsuario = model.NombreUsuario;
                usuario.Contrasena = model.Contrasena;
                


                _context.Usuarios.Update(usuario); // Actualización de la factura en el contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se actualizó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }

        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insertar(Usuario model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            try
            {
                _context.Usuarios.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        public async Task<Usuario> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null

            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(f => f.Id == id);  // Buscar la factura por su ID

            if (usuario == null) return null; // Si la factura no se encontró, devolver null

            return usuario; // Devolver la factura encontrada
        }

        public async Task<IQueryable<Usuario>> LeerTodos()
        {
            IQueryable<Usuario> listaDeUsuarios = _context.Usuarios; // Obtener todas las facturas del contexto

            return listaDeUsuarios; // Devolver la lista de facturas
        }
    }
}