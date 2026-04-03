using Microsoft.AspNetCore.Identity;

namespace restaurante.Models
{
    public class Usuario : IdentityUser<int>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }

        public bool IsAtivo { get; set; }
    }
}