using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using restaurante.Models;
using restaurante.ViewModels;

namespace restaurante.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;

        public ContaController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View(new UsuarioCadastroViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var novoUsuario = new Usuario
                {
                    UserName = model.NomeUsuario,
                    Email = model.NomeUsuario + "@restaurante.com", 
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    CPF = model.CPF,
                    IsAtivo = true
                };

                var resultado = await _userManager.CreateAsync(novoUsuario, model.Senha);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}