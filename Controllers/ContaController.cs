using Microsoft.AspNetCore.Mvc;
using restaurante.Data;
using restaurante.Models;
using restaurante.ViewModels;

namespace restaurante.Controllers
{
    public class ContaController : Controller
    {
        private readonly RestauranteContext _context;

        public ContaController(RestauranteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var model = new UsuarioCadastroViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = _context.Usuarios
                    .Any(u => u.CPF == model.CPF || u.NomeUsuario == model.NomeUsuario);

                if (usuarioExistente)
                {
                    ModelState.AddModelError("", "CPF ou Usuário já cadastrados.");
                    return View(model);
                }

                var novoUsuario = new Usuario
                {
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    NomeUsuario = model.NomeUsuario,
                    CPF = model.CPF,
                    Perfil = Usuario.TipoPerfil.Comum,
                    IsAtivo = true,
                    SenhaHash = model.Senha
                };

                _context.Usuarios.Add(novoUsuario);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            // Se houver erro de validação, devolvemos o modelo com os erros
            return View(model);
        }
    }
}