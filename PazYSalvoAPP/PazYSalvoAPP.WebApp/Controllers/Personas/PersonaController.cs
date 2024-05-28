using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;

namespace PazYSalvoAPP.WebApp.Controllers.Personas
{
    public class PersonaController : Controller
    {
        private readonly IPersonaService _personaService;
        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarPersonas()
        {
            IQueryable<Persona>? consultaDePersonas = await _personaService.LeerTodos();

            List<PersonaViewModel> listadoDePersonas = consultaDePersonas.Select(e => new PersonaViewModel
            {

                Id = e.Id,
                Nombres = e.Nombres,
                Telefono  = e.Telefono,
                CorreoElectronico =e.CorreoElectronico,
                DocumentoIdentificacion =e.DocumentoIdentificacion,
               
                

            }).ToList();

            return PartialView("_ListadoDePersona",
                              listadoDePersonas);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarPersonas([FromBody] PersonaViewModel model)
        {
            Persona persona = new Persona()
            {
                Nombres = model.Nombres,
                Telefono = model.Telefono,
                CorreoElectronico = model.CorreoElectronico,
                DocumentoIdentificacion = model.DocumentoIdentificacion
            };

            bool response = await _personaService.Insertar(persona);

            if (response)
            {

                return Json(new { success = true, message = "Persona agregada con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar persona" });
            }

        }
         public async Task<IActionResult> EditarPersona(int id)
        {
            var persona = await _personaService.Leer(id);
            PersonaViewModel personaAEditar = new PersonaViewModel()
            {
                Id = persona.Id,
                Nombres = persona.Nombres,
                Telefono = persona.Telefono,
                CorreoElectronico = persona.CorreoElectronico,
                DocumentoIdentificacion = persona.DocumentoIdentificacion
            };
                                    

            return View("EditarPersona", personaAEditar);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarPersona(PersonaViewModel model)
        {
            Persona personaAEditar = await _personaService.Leer(model.Id);
            if (personaAEditar == null)
            {
                TempData["ErrorMessage"] = "Persona no encontrada";
                return RedirectToAction("EditarPersonas", new { id = model.Id });
            }

            Persona persona = new Persona()
            {
                Id = model.Id,
                Nombres = model.Nombres == null ? personaAEditar.Nombres : model.Nombres,
                Telefono = model.Telefono == null ? personaAEditar.Telefono : model.Telefono,
                CorreoElectronico = model.CorreoElectronico == null ? personaAEditar.CorreoElectronico : model.CorreoElectronico,
                DocumentoIdentificacion = model.DocumentoIdentificacion == null ? personaAEditar.DocumentoIdentificacion : model.DocumentoIdentificacion

            };

            bool response = await _personaService.Actualizar(persona);

            if (response)
            {
                return RedirectToAction("Index", "Persona");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al actualizar persona";
                return RedirectToAction("EditarPersona", new { id = model.Id });
            }
        }
    }
}