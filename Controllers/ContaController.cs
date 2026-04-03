using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using restaurante.Models;
using restaurante.ViewModels;

namespace restaurante.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

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
                    // Login automático após o cadastro
                    await _signInManager.SignInAsync(novoUsuario, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // --- LOGIN ---
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(
                    model.NomeUsuario,
                    model.Senha,
                    isPersistent: false,
                    lockoutOnFailure: false);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["ErroLogin"] = "Usuário ou senha incorretos.";
            return RedirectToAction("Index", "Home");
        }

        // --- LOGOUT ---
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Limpa o cookie de autenticação
            return RedirectToAction("Index", "Home");
        }

        // --- BUSCAR DADOS PARA O PERFIL ---
        [HttpGet]
        public async Task<IActionResult> ObterDadosPerfil()
        {
            var usuario = await _userManager.GetUserAsync(User);

            // RESOLVE USUÁRIO FANTASMA: Se o cookie existe mas o banco foi resetado, desloga
            if (usuario == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            var model = new PerfilViewModel
            {
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                NomeUsuario = usuario.UserName,
                CPF = usuario.CPF
            };

            return PartialView("_MeusDados", model);
        }

        // --- ATUALIZAR DADOS E SENHA ---
        [HttpPost]
        public async Task<IActionResult> AtualizarPerfil(PerfilViewModel model)
        {
            var usuario = await _userManager.GetUserAsync(User);

            // Proteção contra usuário inexistente no banco
            if (usuario == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            // 1. ATUALIZAR NOME DE USUÁRIO (USER NAME)
            if (usuario.UserName != model.NomeUsuario)
            {
                var resultadoNome = await _userManager.SetUserNameAsync(usuario, model.NomeUsuario);
                if (!resultadoNome.Succeeded)
                {
                    TempData["Erro"] = "Este nome de usuário já está em uso.";
                    return RedirectToAction("Index", "Home");
                }
            }

            // 2. ATUALIZAR DADOS BÁSICOS E CPF
            usuario.Nome = model.Nome;
            usuario.Sobrenome = model.Sobrenome;
            usuario.CPF = model.CPF;

            var resultadoUpdate = await _userManager.UpdateAsync(usuario);

            if (resultadoUpdate.Succeeded)
            {
                // 3. LÓGICA DE SENHA (DESLOGA SE FOR ALTERADA)
                if (!string.IsNullOrEmpty(model.NovaSenha))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    var resultadoSenha = await _userManager.ResetPasswordAsync(usuario, token, model.NovaSenha);

                    if (resultadoSenha.Succeeded)
                    {
                        // Desloga automaticamente para validar a nova senha
                        await _signInManager.SignOutAsync();
                        TempData["Mensagem"] = "Dados e Senha alterados! Por favor, faça login novamente.";
                        return RedirectToAction("Index", "Home");
                    }
                }

                // 4. ATUALIZAR COOKIE
                // Se mudou apenas dados (Username/CPF) sem mudar a senha, atualiza o cookie para manter logado
                await _signInManager.RefreshSignInAsync(usuario);
                TempData["Mensagem"] = "Perfil atualizado com sucesso!";
            }

            return RedirectToAction("Index", "Home");
        }
    }
} 