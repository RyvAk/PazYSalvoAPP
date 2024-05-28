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
    public class MediosDePagoService : IMediosDePagoService
    {
        // 1- Establecer conexión
        // Declaración de un campo privado readonly llamado _context de tipo PazSalvoContext
        private readonly PazSalvoContext _context;

        // Constructor de la clase que recibe una instancia de PazSalvoContext
        public MediosDePagoService(PazSalvoContext context)
        {
            _context = context; // Asignación de la instancia de PazSalvoContext al campo _context
        }

        // Método para actualizar una factura en la base de datos
        public async Task<bool> Actualizar(MediosDePago model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            int mediosDePagoId = model.Id;

            if (mediosDePagoId == 0 || mediosDePagoId == null) return result;

            try
            {
                MediosDePago mediosDePago = await Leer(mediosDePagoId);

                mediosDePago.Nombre = model.Nombre;
                mediosDePago.Descripcion = model.Descripcion;
              
                _context.MediosDePagos.Update(mediosDePago); // Actualización de la factura en el contexto
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

            var mediosdepagos = _context.MediosDePagos.FirstOrDefault(f => f.Id == id); // Buscar la factura por su ID

            if (mediosdepagos == null) return result; // Si la factura no se encontró, devolver el valor por defecto de result (false)

            try
            {
                _context.MediosDePagos.Remove(mediosdepagos); // Eliminar la factura del contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se eliminó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para insertar una nueva factura en la base de datos
        public async Task<bool> Insertar(MediosDePago model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            try
            {
                _context.MediosDePagos.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para leer una factura de la base de datos por su ID
        public async Task<MediosDePago> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null

            var mediosDePago = _context.MediosDePagos.FirstAsync(f => f.Id == id); // Buscar la factura por su ID

            if (mediosDePago == null) return null; // Si la factura no se encontró, devolver null

            return await mediosDePago; // Devolver la factura encontrada
        }

        // Método para leer todas las facturas de la base de datos
        public async Task<IQueryable<MediosDePago>> LeerTodos()
        {
            IQueryable<MediosDePago> listaDeMediosDePagos = _context.MediosDePagos; // Obtener todas las facturas del contexto

            return listaDeMediosDePagos; // Devolver la lista de facturas
        }


    }
}
