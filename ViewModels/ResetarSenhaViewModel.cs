using System.ComponentModel.DataAnnotations;

namespace restaurante.ViewModels
{
    public class ResetarSenhaViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Sobrenome é obrigatório.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        public string Telefone { get; set; }

        public string NovoNomeUsuario { get; set; } // Opcional

        [Required(ErrorMessage = "A Nova Senha é obrigatória.")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "A Confirmação é obrigatória.")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
