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
    public class PersonaService : IPersonaService
    { // 1- Establecer conexión
        // Declaración de un campo privado readonly llamado _context de tipo PazSalvoContext
        private readonly PazSalvoContext _context;

        // Constructor de la clase que recibe una instancia de PazSalvoContext
        public PersonaService(PazSalvoContext context)
        {
            _context = context; // Asignación de la instancia de PazSalvoContext al campo _context
        }

        // Método para actualizar una factura en la base de datos
        public async Task<bool> Actualizar(Persona model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            int personaId = model.Id;

            if (personaId == 0 || personaId == null) return result;

            try
            {
                Persona persona = await Leer(personaId);

 
                persona.Nombres = model.Nombres;
                persona.Telefono = model.Telefono;
                persona.CorreoElectronico = model.CorreoElectronico;
                persona.DocumentoIdentificacion = model.DocumentoIdentificacion;

               
                _context.Personas.Update(persona); // Actualización de la factura en el contexto
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

            var personas = _context.Personas.FirstOrDefault(f => f.Id == id); // Buscar la persona por su ID

            if (personas == null) return result; // Si la factura no se encontró, devolver el valor por defecto de result (false)

            try
            {
                _context.Personas.Remove(personas); // Eliminar cliente del contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se eliminó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para insertar una nueva factura en la base de datos
        public async Task<bool> Insertar(Persona model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            try
            {
                _context.Personas.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para leer una factura de la base de datos por su ID
        public async Task<Persona> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null

            var persona = _context.Personas.FirstAsync(f => f.Id == id); // Buscar la factura por su ID

            if (persona == null) return null; // Si la factura no se encontró, devolver null

            return await persona; // Devolver la factura encontrada
        }

        // Método para leer todas las facturas de la base de datos
        public async Task<IQueryable<Persona>> LeerTodos()
        {
            IQueryable<Persona> listaDePersonas = _context.Personas; // Obtener todas las facturas del contexto

            return listaDePersonas; // Devolver la lista de facturas
        }


    }
}
