using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;

namespace PazYSalvoAPP.WebApp.Controllers.MediosDePagos
{
    public class MediosDePagoController : Controller
    {
        private readonly IMediosDePagoService _mediosDePagoService;
        public MediosDePagoController(IMediosDePagoService mediosDePagoService)
        {
            _mediosDePagoService = mediosDePagoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarMediosDePagos()
        {
            IQueryable<MediosDePago>? consultaDeMediosDePagos = await _mediosDePagoService.LeerTodos();

            List<MediosDePagoViewModel> listadoDeMediosDePagos = consultaDeMediosDePagos.Select(e => new MediosDePagoViewModel
            {

                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion  = e.Descripcion ,
               

            }).ToList();

            return PartialView("_ListadoDeMediosDePago",
                              listadoDeMediosDePagos);
        }
        
        [HttpPost]
        public async Task<IActionResult> AgregarMediosDepagos([FromBody] MediosDePagoViewModel model)
        {
            MediosDePago mediosDePago = new MediosDePago()
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion
            };

            bool response = await _mediosDePagoService.Insertar(mediosDePago);

            if (response)
            {

                return Json(new { success = true, message = "Medio de pago agregada con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar medio de pago" });
            }

        }
        public async Task<IActionResult> EditarMediosDePago(int id)
        {
            var medioDePago = await _mediosDePagoService.Leer(id);
            MediosDePagoViewModel mediosDePagoAEditar = new MediosDePagoViewModel()
            {
                Id = medioDePago.Id,
                Nombre = medioDePago.Nombre,
                Descripcion = medioDePago.Descripcion
            };


            return View("EditarMediosDePago", mediosDePagoAEditar);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarMediosDePago(MediosDePagoViewModel model)
        {
            MediosDePago mediosDePagoAEditar = await _mediosDePagoService.Leer(model.Id);
            if (mediosDePagoAEditar == null)
            {
                TempData["ErrorMessage"] = "Medios de pago no encontrado";
                return RedirectToAction("EditarMediosDePagos", new { id = model.Id });
            }

            MediosDePago mediosDePago = new MediosDePago()
            {
                Id = model.Id,
                Nombre = model.Nombre == null ? mediosDePagoAEditar.Nombre : model.Nombre,
                Descripcion = model.Descripcion == null ? mediosDePagoAEditar.Descripcion : model.Descripcion
            };

            bool response = await _mediosDePagoService.Actualizar(mediosDePago);

            if (response)
            {
                return RedirectToAction("Index", "MediosDePago");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al actualizar medio de pago";
                return RedirectToAction("EditarMediosDePago", new { id = model.Id });
            }
        }
    }
}