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
        [Display(Name = "Nome de Usuário")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A Confirmação de Senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; }
    }
}
