using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;

namespace PazYSalvoAPP.WebApp.Controllers.Clientes
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            IQueryable<Cliente>? consultaDeClientes = await _clienteService.LeerTodos();

            List<ClienteViewModel> listadoDeClientes = consultaDeClientes.Select(e => new ClienteViewModel
            {
                Id = e.Id,
                PersonaId = e.PersonaId,
                RolId = e.RolId,

            }).ToList();

            return PartialView("_ListadoDeCliente",
                              listadoDeClientes);
        }
         [HttpPost]
        public async Task<IActionResult> AgregarClientes([FromBody] ClienteViewModel model)
        {
            Cliente cliente = new Cliente()
            {
                PersonaId = model.PersonaId,
                RolId = model.RolId,
               
            };

            bool response = await _clienteService.Insertar(cliente);

            if (response)
            {

                return Json(new { success = true, message = "Cliente agregado con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar cliente" });
            }

        }

        public async Task<IActionResult> EditarCliente(int id)
        {
            var cliente = await _clienteService.Leer(id);
             if (cliente == null)
    {
        // Muestra una alerta en el navegador si el cliente no se encuentra
        TempData["ErrorMessage"] = "El cliente que intentas editar no fue encontrado.";

        return RedirectToAction("Index");
    }


            ClienteViewModel clienteAEditar = new ClienteViewModel()
            {
                PersonaId = cliente.PersonaId,
                RolId = cliente.RolId,
                
            };


            return View("EditarCliente", clienteAEditar);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarCliente(ClienteViewModel model)
        {
            Cliente clienteAEditar = await _clienteService.Leer(model.Id);
            if (clienteAEditar == null)
            {
                TempData["ErrorMessage"] = "Cliente no encontrado";
                return RedirectToAction("EditarCliente", new { id = model.Id });
            }

            Cliente cliente = new Cliente()
            {
               
                PersonaId = model.PersonaId == null ? clienteAEditar.PersonaId : model.PersonaId,
                RolId = model.RolId == null ? clienteAEditar.RolId : model.RolId
                
            };

            bool response = await _clienteService.Actualizar(cliente);

            if (response)
            {
                return RedirectToAction("Index", "Cliente");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al actualizar cliente";
                return RedirectToAction("EditarCliente", new { id = model.Id });
            }
        }
    }
}

     