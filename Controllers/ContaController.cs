using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.Models;
using restaurante.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RestauranteContext _context;

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
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioCadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var erros = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new { mensagem = "Verifique os dados informados:\n" + erros });
            }

            // Verificações de Duplicidade específicas para evitar erros no banco
            if (await _context.Usuarios.AnyAsync(u => u.CPF == model.CPF))
                return BadRequest(new { mensagem = "Atenção: Este CPF já está cadastrado!" });

            if (await _context.Usuarios.AnyAsync(u => u.Email == model.Email))
                return BadRequest(new { mensagem = "Atenção: Este E-mail já está sendo usado!" });

            if (await _context.Usuarios.AnyAsync(u => u.PhoneNumber == model.Telefone))
                return BadRequest(new { mensagem = "Atenção: Este Telefone já está cadastrado!" });

            if (await _context.Usuarios.AnyAsync(u => u.UserName == model.NomeUsuario))
                return BadRequest(new { mensagem = "Atenção: Este Nome de Usuário não está disponível!" });

            var novoUsuario = new Usuario
            {
                UserName = model.NomeUsuario,
                Email = model.Email,
                PhoneNumber = model.Telefone,
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
                TempData["MensagemErroSistema"] = "Sucesso! Sua conta foi criada e você já está logado.";
                return Ok();
            }

            // Captura erros de complexidade de senha definidos no Program.cs
            var erroMsg = string.Join("\n", resultado.Errors.Select(e => e.Description));
            return BadRequest(new { mensagem = "Sua senha não cumpre os requisitos:\n\n" + erroMsg });
        }

        [HttpPost]
        public async Task<IActionResult> ResetarSenha([FromBody] ResetarSenhaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var erros = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new { mensagem = "Preencha os campos:\n" + erros });
            }

            // 1. Verificação de Identidade: Todos os campos precisam bater exatamente
            var user = await _context.Usuarios.FirstOrDefaultAsync(u =>
                u.CPF == model.CPF &&
                u.Email == model.Email &&
                u.PhoneNumber == model.Telefone &&
                u.Nome.ToLower() == model.Nome.ToLower() &&
                u.Sobrenome.ToLower() == model.Sobrenome.ToLower());

            if (user == null)
            {
                return BadRequest(new { mensagem = "Dados inválidos: As informações informadas não conferem com nossa base de dados." });
            }

            // 2. Trava de segurança: Não deixa repetir a senha antiga
            if (await _userManager.CheckPasswordAsync(user, model.NovaSenha))
            {
                return BadRequest(new { mensagem = "Erro: A nova senha não pode ser igual à senha anterior." });
            }

            // 3. Atualização de Nome de Usuário (Opcional)
            if (!string.IsNullOrWhiteSpace(model.NovoNomeUsuario) && model.NovoNomeUsuario != user.UserName)
            {
                var usernameExiste = await _context.Usuarios.AnyAsync(u => u.UserName == model.NovoNomeUsuario);
                if (usernameExiste)
                {
                    return BadRequest(new { mensagem = "Erro: O novo Nome de Usuário já está sendo usado por outra pessoa." });
                }
                await _userManager.SetUserNameAsync(user, model.NovoNomeUsuario);
            }

            // 4. Executa o Reset da Senha
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resultado = await _userManager.ResetPasswordAsync(user, token, model.NovaSenha);

            if (resultado.Succeeded)
            {
                // 5. Login Automático após o sucesso
                await _signInManager.SignInAsync(user, isPersistent: false);
                TempData["MensagemErroSistema"] = "Sucesso! Sua senha foi redefinida e você já está logado.";
                return Ok();
            }

            var errosIdentity = string.Join("\n", resultado.Errors.Select(e => e.Description));
            return BadRequest(new { mensagem = "Erro de segurança na nova senha:\n" + errosIdentity });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.NomeUsuario);

                if (user != null)
                {
                    if (!user.IsAtivo)
                    {
                        TempData["MensagemErroSistema"] = "Sua conta foi desativada. Por favor, entre em contato com o suporte.";
                        return RedirectToAction("Index", "Home");
                    }

                    var resultado = await _signInManager.PasswordSignInAsync(user, model.Senha, isPersistent: false, lockoutOnFailure: false);

                    if (resultado.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

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
        // 2. GERENCIAMENTO DE PERFIL
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
                        TempData["MensagemErroSistema"] = "Dados e Senha alterados com sucesso! Faça login novamente.";
                        return RedirectToAction("Index", "Home");
                    }
                }

                await _signInManager.RefreshSignInAsync(usuario);
                TempData["MensagemErroSistema"] = "Perfil atualizado com sucesso!";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DesativarConta()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            usuario.IsAtivo = false;
            var resultado = await _userManager.UpdateAsync(usuario);

            if (resultado.Succeeded)
            {
                await _signInManager.SignOutAsync();
                TempData["MensagemErroSistema"] = "Sua conta foi desativada. Esperamos ver você novamente!";
                return Ok();
            }

            return BadRequest("Não foi possível desativar a conta no momento.");
        }

        // ==========================================
        // 3. ENDEREÇOS
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> ObterEnderecos()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

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
                var novo = new Endereco
                {
                    UsuarioId = usuario.Id,
                    Logradouro = model.Logradouro,
                    Numero = model.Numero,
                    Bairro = model.Bairro,
                    CEP = model.CEP,
                    IsAtivo = true
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
                endereco.IsAtivo = false;
                _context.Update(endereco);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}