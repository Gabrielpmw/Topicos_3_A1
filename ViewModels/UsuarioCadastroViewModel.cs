using System.ComponentModel.DataAnnotations;

namespace restaurante.ViewModels
{
    public class UsuarioCadastroViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Sobrenome é obrigatório.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O Nome de Usuário é obrigatório.")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter exatamente 11 números.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "O Telefone deve conter 10 ou 11 números.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A Confirmação de Senha é obrigatória.")]
        public string ConfirmarSenha { get; set; }
    }
}