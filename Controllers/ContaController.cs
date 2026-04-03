using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using restaurante.Models;
using restaurante.ViewModels;

namespace restaurante.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager; // Adicionado para gerenciar o login

        // Atualizamos o construtor para receber o SignInManager
        public ContaController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // --- CADASTRO ---

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
                    // LOGIN AUTOMÁTICO: Após cadastrar, o usuário já entra logado
                    await _signInManager.SignInAsync(novoUsuario, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // Se der erro no cadastro pelo modal, ele volta para a Home avisando
            return RedirectToAction("Index", "Home");
        }

        // --- LOGIN ---

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Tenta realizar o login com o nome de usuário e senha
                var resultado = await _signInManager.PasswordSignInAsync(
                    model.NomeUsuario,
                    model.Senha,
                    isPersistent: false,
                    lockoutOnFailure: false);

                if (resultado.Succeeded)
                {
                    // Se deu certo, volta para a Home onde o Header já mostrará "Olá, Gabriel"
                    return RedirectToAction("Index", "Home");
                }
            }

            // Se o login falhar, voltamos para a Home. 
            // Você pode usar o TempData para mostrar uma mensagem de erro se quiser.
            TempData["ErroLogin"] = "Usuário ou senha incorretos.";
            return RedirectToAction("Index", "Home");
        }

        // --- LOGOUT ---

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}