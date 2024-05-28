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
    public class PagoService : IPagoService
    {
        // 1- Establecer conexión
        // Declaración de un campo privado readonly llamado _context de tipo PazSalvoContext
        private readonly PazSalvoContext _context;

        // Constructor de la clase que recibe una instancia de PazSalvoContext
        public PagoService(PazSalvoContext context)
        {
            _context = context; // Asignación de la instancia de PazSalvoContext al campo _context
        }

        // Método para actualizar una factura en la base de datos
        public async Task<bool> Actualizar(Pago model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            int pagoId = model.Id;

            if (pagoId == 0 || pagoId == null) return result;

            try
            {
                Pago pago = await Leer(pagoId);


                pago.MontoDePago = model.MontoDePago;
                pago.FacturaId = model.FacturaId;
                pago.Activo = model.Activo;
               


                _context.Pagos.Update(pago); // Actualización de la factura en el contexto
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
            throw new NotImplementedException();
        }
 

        // Método para insertar una nueva factura en la base de datos
        public async Task<bool> Insertar(Pago model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            try
            {
                _context.Pagos.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para leer una factura de la base de datos por su ID
        public async Task<Pago> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null

            var pago = _context.Pagos.FirstAsync(f => f.Id == id); // Buscar la factura por su ID

            if (pago == null) return null; // Si la factura no se encontró, devolver null

            return await pago; // Devolver la factura encontrada
        }

        // Método para leer todas las facturas de la base de datos
        public async Task<IQueryable<Pago>> LeerTodos()
        {
            IQueryable<Pago> listaDePagos = _context.Pagos; // Obtener todas las facturas del contexto

            return listaDePagos; // Devolver la lista de facturas
        }


    }
}
