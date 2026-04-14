using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.Models;
using restaurante.ViewModels;

namespace restaurante.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RestauranteContext _context; // Adicionado para acessar os Endereços

        // Construtor atualizado com o contexto do banco de dados
        public ContaController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RestauranteContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // ==========================================
        // 1. AUTENTICAÇÃO E CADASTRO
        // ==========================================

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
                    await _userManager.AddToRoleAsync(novoUsuario, "Comum");
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

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Busca o usuário pelo nome de usuário digitado
                var user = await _userManager.FindByNameAsync(model.NomeUsuario);

                if (user != null)
                {
                    // 2. A MÁGICA ACONTECE AQUI: Verifica se a conta está desativada
                    if (!user.IsAtivo)
                    {
                        // Guarda a mensagem de erro temporariamente para exibir na tela
                        TempData["MensagemErroSistema"] = "Sua conta foi desativada. Por favor, entre em contato com o suporte do site.";
                        return RedirectToAction("Index", "Home"); // Redireciona de volta para a tela inicial
                    }

                    // 3. Se estiver ativo, tenta fazer o login padrão com a senha
                    var resultado = await _signInManager.PasswordSignInAsync(user, model.Senha, isPersistent: false, lockoutOnFailure: false);

                    if (resultado.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            // Se o usuário não existir ou a senha estiver errada
            TempData["MensagemErroSistema"] = "Usuário ou senha inválidos.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // ==========================================
        // 2. GERENCIAMENTO DE PERFIL (MEUS DADOS)
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> ObterDadosPerfil()
        {
            var usuario = await _userManager.GetUserAsync(User);

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

        [HttpPost]
        public async Task<IActionResult> AtualizarPerfil(PerfilViewModel model)
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            if (usuario.UserName != model.NomeUsuario)
            {
                var resultadoNome = await _userManager.SetUserNameAsync(usuario, model.NomeUsuario);
                if (!resultadoNome.Succeeded)
                {
                    TempData["Erro"] = "Este nome de usuário já está em uso.";
                    return RedirectToAction("Index", "Home");
                }
            }

            usuario.Nome = model.Nome;
            usuario.Sobrenome = model.Sobrenome;
            usuario.CPF = model.CPF;

            var resultadoUpdate = await _userManager.UpdateAsync(usuario);

            if (resultadoUpdate.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.NovaSenha))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    var resultadoSenha = await _userManager.ResetPasswordAsync(usuario, token, model.NovaSenha);

                    if (resultadoSenha.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        TempData["Mensagem"] = "Dados e Senha alterados! Por favor, faça login novamente.";
                        return RedirectToAction("Index", "Home");
                    }
                }

                await _signInManager.RefreshSignInAsync(usuario);
                TempData["Mensagem"] = "Perfil atualizado com sucesso!";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DesativarConta()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return Unauthorized();
            }

            // Aplica o Soft Delete
            usuario.IsAtivo = false;
            var resultado = await _userManager.UpdateAsync(usuario);

            if (resultado.Succeeded)
            {
                // Desloga o usuário imediatamente
                await _signInManager.SignOutAsync();

                // Usa o nosso sistema de alertas do layout para exibir a mensagem na tela inicial
                TempData["MensagemErroSistema"] = "Sua conta foi desativada com sucesso. Esperamos ver você novamente no futuro!";
                return Ok();
            }

            return BadRequest("Não foi possível desativar a conta no momento.");
        }

        // ==========================================
        // 3. GERENCIAMENTO DE ENDEREÇOS
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> ObterEnderecos()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            // Busca apenas os endereços ativos do usuário logado
            var enderecos = _context.Enderecos
                .Where(e => e.UsuarioId == usuario.Id && e.IsAtivo)
                .Select(e => new EnderecoViewModel
                {
                    Id = e.Id,
                    Logradouro = e.Logradouro,
                    Numero = e.Numero,
                    Bairro = e.Bairro,
                    CEP = e.CEP
                }).ToList();

            return PartialView("_MeusEnderecos", enderecos);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarEndereco([FromBody] EnderecoViewModel model)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            if (model.Id.HasValue && model.Id.Value > 0)
            {
                // ATUALIZAR
                var enderecoDb = _context.Enderecos.FirstOrDefault(e => e.Id == model.Id && e.UsuarioId == usuario.Id);
                if (enderecoDb != null)
                {
                    enderecoDb.Logradouro = model.Logradouro;
                    enderecoDb.Numero = model.Numero;
                    enderecoDb.Bairro = model.Bairro;
                    enderecoDb.CEP = model.CEP;
                    _context.Update(enderecoDb);
                }
            }
            else
            {
                // CRIAR NOVO
                var novo = new Endereco
                {
                    UsuarioId = usuario.Id,
                    Logradouro = model.Logradouro,
                    Numero = model.Numero,
                    Bairro = model.Bairro,
                    CEP = model.CEP,
                    IsAtivo = true // Define como ativo por padrão
                };
                _context.Add(novo);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoverEndereco(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            var endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id && e.UsuarioId == usuario.Id);
            if (endereco != null)
            {
                // Soft Delete: Apenas desativa o endereço em vez de apagar do banco
                endereco.IsAtivo = false;
                _context.Update(endereco);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}