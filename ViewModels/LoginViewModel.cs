using System.ComponentModel.DataAnnotations;

namespace restaurante.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool LembrarMe { get; set; }
    }
}
