using Microsoft.EntityFrameworkCore;
using PazYSalvoAPP.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public class ServicioService : IServicioService
    {
        // 1- Instancia al contexto => Para conectarme a BD
        private readonly PazSalvoContext _context;
        public ServicioService(PazSalvoContext context)
        {
            _context = context;
        }

        public async Task<bool> Actualizar(Servicio model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            int servicioId = model.Id;

            if (servicioId == 0 || servicioId == null) return result;

            try
            {
                Servicio servicio = await Leer(servicioId);

                servicio.Nombre = model.Nombre;
                servicio.Descripcion = model.Descripcion;
                servicio.Precio = model.Precio;

                _context.Servicios.Update(servicio); // Actualización de la factura en el contexto
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

        public async Task<bool> Insertar(Servicio model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            try
            {
                _context.Servicios.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        public async Task<Servicio> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null

            Servicio servicio = await _context.Servicios.FirstOrDefaultAsync(f => f.Id == id);  // Buscar la factura por su ID

            if (servicio == null) return null; // Si la factura no se encontró, devolver null

            return servicio; // Devolver la factura encontrada
        }

        public async Task<IQueryable<Servicio>> LeerTodos()
        {
            IQueryable<Servicio> listaDeServicios = _context.Servicios; // Obtener todas las facturas del contexto

            return listaDeServicios; // Devolver la lista de facturas
        }
    }
}
