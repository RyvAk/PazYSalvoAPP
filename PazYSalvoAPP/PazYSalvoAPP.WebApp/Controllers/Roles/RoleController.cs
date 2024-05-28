using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;

namespace PazYSalvoAPP.WebApp.Controllers.Roles
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarRoles()
        {
            IQueryable<Role>? consultaDeRoles = await _roleService.LeerTodos();

            List<RoleViewModel> listadoDeRoles = consultaDeRoles.Select(e => new RoleViewModel
            {

                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Activo =e.Activo,
                FechaDeCreacion=e.FechaDeCreacion,


            }).ToList();

            return PartialView("_ListadoDeRole",
                              listadoDeRoles);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarRoles([FromBody] RoleViewModel model)
        { 
            

            Role role = new Role()
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo
                
            };

            bool response = await _roleService.Insertar(role);

            if (response)
            {

                return Json(new { success = true, message = "Rol agregada con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar rol" });
            }

        }
        public async Task<IActionResult> EditarRole(int id)
        {
            var role = await _roleService.Leer(id);
            RoleViewModel roleAEditar = new RoleViewModel()
            {
                Id = role.Id,
                Nombre = role.Nombre,
                Descripcion = role.Descripcion,
                Activo = role.Activo
                
            };


            return View("EditarRole", roleAEditar);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarRole(RoleViewModel model)
        {
            Role roleAEditar = await _roleService.Leer(model.Id);
            if (roleAEditar == null)
            {
                TempData["ErrorMessage"] = "Rol no encontrado";
                return RedirectToAction("EditarRoles", new { id = model.Id });
            }

            Role role = new Role()
            {
                Id = model.Id,
                Nombre = model.Nombre == null ? roleAEditar.Nombre : model.Nombre,
                Descripcion = model.Descripcion == null ? roleAEditar.Descripcion : model.Descripcion,
                Activo = model.Activo == null ? roleAEditar.Activo : model.Activo
                

            };

            bool response = await _roleService.Actualizar(role);

            if (response)
            {
                return RedirectToAction("Index", "Role");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al actualizar rol";
                return RedirectToAction("EditarRole", new { id = model.Id });
            }
        }
    }
}