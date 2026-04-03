using System.ComponentModel.DataAnnotations;

namespace restaurante.ViewModels
{
    public class PerfilViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeUsuario { get; set; }
        public string CPF { get; set; }

        [DataType(DataType.Password)]
        public string? NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem")]
        public string? ConfirmarNovaSenha { get; set; }
    }
}
