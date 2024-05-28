using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;

namespace PazYSalvoAPP.WebApp.Controllers.Estados
{
    public class EstadoController : Controller
    {
        private readonly IEstadoService _estadoService;
        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarEstados()
        {
            IQueryable<Estado>? consultaDeEstados = await _estadoService.LeerTodos();

            List<EstadoViewModel> listadoDeEstados = consultaDeEstados.Select(e => new EstadoViewModel
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,

            }).ToList();

            return PartialView("_ListadoDeEstado",
                              listadoDeEstados);
        }
        //[HttpPost] // *
        //public async Task<IActionResult> AgregarFacturas([FromBody] FacturaViewModel model)
        //{
        //    Estado estado = new estado()
        //    {

        //        Nombre = e.Nombre,
        //        Descripcion = e.Descripcion,
        //    };

        //    bool response = await _estadoService.Insertar(estado);

        //    return RedirectToAction("Index");

        //}

        //[HttpPost]
        //public async Task<IActionResult> ActualizarFacturas([FromBody] FacturaViewModel model)
        //{
        //    Factura factura = new Factura()
        //    {


        //    };

        //    bool response = await _estadoService.Actualizar(factura);

        //    return StatusCode(StatusCodes.Status200OK,
        //                      new { valor = response });

        //}
    }
}
