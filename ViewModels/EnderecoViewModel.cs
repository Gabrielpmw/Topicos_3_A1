using System.ComponentModel.DataAnnotations;

namespace restaurante.ViewModels
{
    public class EnderecoViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter exatamente 8 números.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "O CEP deve conter apenas números.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "A Quadra / ARSE / ARSO é obrigatória.")]
        public string Quadra { get; set; }

        [Required(ErrorMessage = "A Alameda / QI é obrigatória.")]
        public string Alameda { get; set; }

        [Required(ErrorMessage = "O Lote é obrigatório.")]
        public string Lote { get; set; }

        // Complemento é o único campo que faz sentido ser opcional
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O Ponto de Referência é obrigatório.")]
        public string Referencia { get; set; }
    }
}
