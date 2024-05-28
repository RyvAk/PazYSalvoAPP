using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;

namespace PazYSalvoAPP.WebApp.Controllers.Pagos
{
    public class PagoController : Controller
    {
        private readonly IPagoService _pagoService;
        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarPagos()
        {
            IQueryable<Pago>? consultaDePagos = await _pagoService.LeerTodos();

            List<PagoViewModel> listadoDePagos = consultaDePagos.Select(e => new PagoViewModel
            {


                Id = e.Id,
                MontoDePago = e. MontoDePago,
                Activo =e.Activo,
               FacturaId =e.FacturaId,

            }).ToList();

            return PartialView("_ListadoDePagos",
                              listadoDePagos);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarPagos([FromBody] PagoViewModel model)
        {
            Pago pago = new Pago()
            {
                MontoDePago = model.MontoDePago,
                FacturaId = model.FacturaId,
                Activo = model.Activo
            };

            bool response = await _pagoService.Insertar(pago);

            if (response)
            {

                return Json(new { success = true, message = "Pago agregado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar pago" });
            }

        }
        public async Task<IActionResult> EditarPago(int id)
        {
            var pago = await _pagoService.Leer(id);
            PagoViewModel pagoAEditar = new PagoViewModel()
            {
                Id = pago.Id,
                MontoDePago = pago.MontoDePago,
                FacturaId = pago.FacturaId,
                Activo = pago.Activo
            };


            return View("EditarPago", pagoAEditar);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarPago(PagoViewModel model)
        {
            Pago pagoAEditar = await _pagoService.Leer(model.Id);
            if (pagoAEditar == null)
            {
                TempData["ErrorMessage"] = "Pago no encontrada";
                return RedirectToAction("EditarPagos", new { id = model.Id });
            }

            Pago pago = new Pago()
            {
                Id = model.Id,
                MontoDePago = model.MontoDePago == null ? pagoAEditar.MontoDePago : model.MontoDePago,
                FacturaId = model.FacturaId == null ? pagoAEditar.FacturaId : model.FacturaId,
                Activo = model.Activo == null ? pagoAEditar.Activo : model.Activo

            };

            bool response = await _pagoService.Actualizar(pago);

            if (response)
            {
                return RedirectToAction("Index", "Pago");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al actualizar pago";
                return RedirectToAction("EditarPago", new { id = model.Id });
            }
        }
    }
}