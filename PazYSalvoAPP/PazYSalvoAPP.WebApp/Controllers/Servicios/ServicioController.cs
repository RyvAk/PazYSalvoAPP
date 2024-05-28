using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;
using System.Globalization;

namespace PazYSalvoAPP.WebApp.Controllers.Servicios
{
    public class ServicioController : Controller
    {
        private readonly IServicioService _servicioService;
        public ServicioController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarServicios()
        {
            IQueryable<Servicio>? consultaDeServicios = await _servicioService.LeerTodos();

            List<Servicio> listadoDeServicios = consultaDeServicios.Select(f => new Servicio
            {
                Id = f.Id,
                Nombre = f.Nombre,
                Descripcion = f.Descripcion,
                Precio = f.Precio

            }).ToList();

            return PartialView("_ListadoDeServicios",
                              listadoDeServicios);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarServicios([FromBody] ServicioViewModel model)
        {
            Servicio servicio = new Servicio()
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Precio = model.Precio
            };

            bool response = await _servicioService.Insertar(servicio);

            if (response)
            {

                return Json(new { success = true, message = "Servicio agregada con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar servicio" });
            }

        }

        public async Task<IActionResult> EditarServicio(int id)
        {
            var servicio = await _servicioService.Leer(id);
            ServicioViewModel servicioAEditar = new ServicioViewModel()
            {
                Id = servicio.Id,
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion,
                Precio = servicio.Precio
            };
                                    

            return View("EditarServicio", servicioAEditar);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarServicio(ServicioViewModel model)
        {
            Servicio servicioAEditar = await _servicioService.Leer(model.Id);
            if (servicioAEditar == null)
            {
                TempData["ErrorMessage"] = "Factura no encontrada";
                return RedirectToAction("EditarFacturas", new { id = model.Id });
            }

            Servicio servicio = new Servicio()
            {
                Id = model.Id,
                Nombre = model.Nombre == null ? servicioAEditar.Nombre : model.Nombre,
                Descripcion = model.Descripcion == null ? servicioAEditar.Descripcion : model.Descripcion,
                Precio = model.Precio == null ? servicioAEditar.Precio : model.Precio
                                
            };

            bool response = await _servicioService.Actualizar(servicio);

            if (response)
            {
                return RedirectToAction("Index", "Servicio");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al actualizar servicio";
                return RedirectToAction("EditarServicio", new { id = model.Id });
            }
        }
    }
}
