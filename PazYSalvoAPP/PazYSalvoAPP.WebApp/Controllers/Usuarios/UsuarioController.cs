﻿using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;

namespace PazYSalvoAPP.WebApp.Controllers.Usuarios
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            IQueryable<Usuario>? consultaDeUsuarios = await _usuarioService.LeerTodos();

            List<Usuario> listadoDeUsuarios = consultaDeUsuarios.Select(f => new Usuario
            {
                Id = f.Id,
                PersonaId = f.PersonaId,
                NombreUsuario = f.NombreUsuario,
                Contrasena = f.Contrasena
                
            }).ToList();

            return PartialView("_ListadoDeUsuarios",
                              listadoDeUsuarios);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarUsuarios([FromBody] UsuarioViewModel model)
        {
            Usuario usuario = new Usuario()
            {
                PersonaId = model.PersonaId,
                NombreUsuario = model.NombreUsuario,
                Contrasena = model.Contrasena
               
            };

            bool response = await _usuarioService.Insertar(usuario);

            if (response)
            {

                return Json(new { success = true, message = "Usuario agregada con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar usuario" });
            }

        }
        public async Task<IActionResult> EditarUsuario(int id)
        {
            var usuario = await _usuarioService.Leer(id);
            UsuarioViewModel usuarioAEditar = new UsuarioViewModel()
            {
                Id = usuario.Id,
                PersonaId = usuario.PersonaId,
                NombreUsuario = usuario.NombreUsuario,
                Contrasena = usuario.Contrasena
              
            };


            return View("EditarUsuario", usuarioAEditar);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarUsuario(UsuarioViewModel model)
        {
            Usuario usuarioAEditar = await _usuarioService.Leer(model.Id);
            if (usuarioAEditar == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrada";
                return RedirectToAction("EditarUsuarios", new { id = model.Id });
            }

            Usuario usuario = new Usuario()
            {
                Id = model.Id,
                PersonaId = model.PersonaId == null ? usuarioAEditar.PersonaId : model.PersonaId,
                NombreUsuario = model.NombreUsuario == null ? usuarioAEditar.NombreUsuario : model.NombreUsuario,
                Contrasena = model.Contrasena == null ? usuarioAEditar.Contrasena : model.Contrasena
               

            };

            bool response = await _usuarioService.Actualizar(usuario);

            if (response)
            {
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al actualizar usuario";
                return RedirectToAction("EditarUsuario", new { id = model.Id });
            }
        }
    }
}