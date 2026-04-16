using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace restaurante.Models
{
    public class Usuario : IdentityUser<int>
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O sobrenome deve ter no máximo 50 caracteres.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve estar no formato 000.000.000-00.")]
        public string CPF { get; set; }

        public bool IsAtivo { get; set; }
    }
}