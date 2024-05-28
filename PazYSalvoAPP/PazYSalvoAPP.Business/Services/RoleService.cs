using PazYSalvoAPP.Models;
using PazYSalvoAPP.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PazYSalvoAPP.Business.Services
{
    public class RoleService : IRoleService
    {
        // 1- Establecer conexión
        // Declaración de un campo privado readonly llamado _context de tipo PazSalvoContext
        private readonly PazSalvoContext _context;

        // Constructor de la clase que recibe una instancia de PazSalvoContext
        public RoleService(PazSalvoContext context)
        {
            _context = context; // Asignación de la instancia de PazSalvoContext al campo _context
        }

        // Método para actualizar una factura en la base de datos
        public async Task<bool> Actualizar(Role model)
       {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            int roleId = model.Id;

            if (roleId == 0 || roleId == null) return result;

            try
            {
                Role role = await Leer(roleId);


                role.Nombre = model.Nombre;
                role.Descripcion = model.Descripcion;
                role.Activo = model.Activo;
                


                _context.Roles.Update(role); // Actualización de la factura en el contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se actualizó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }


        // Método para eliminar una factura de la base de datos por su ID
        public async Task<bool> Eliminar(int id)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            if (id == default(int)) return result;

            var roles = _context.Roles.FirstOrDefault(f => f.Id == id); // Buscar la factura por su ID

            if (roles == null) return result; // Si la factura no se encontró, devolver el valor por defecto de result (false)

            try
            {
                _context.Roles.Remove(roles); // Eliminar la factura del contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se eliminó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para insertar una nueva factura en la base de datos
        public async Task<bool> Insertar(Role model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            try
            {
                _context.Roles.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para leer una factura de la base de datos por su ID
        public async Task<Role> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null

            var role = _context.Roles.FirstAsync(f => f.Id == id); // Buscar la factura por su ID

            if (role == null) return null; // Si la factura no se encontró, devolver null

            return await role; // Devolver la factura encontrada
        }

        // Método para leer todas las facturas de la base de datos
        public async Task<IQueryable<Role>> LeerTodos()
        {
            IQueryable<Role> listaDeRoles = _context.Roles; // Obtener todas las facturas del contexto

            return listaDeRoles; // Devolver la lista de facturas
        }


    }
}
