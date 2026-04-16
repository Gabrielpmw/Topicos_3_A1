using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace restaurante.ViewModels
{
    public class ItemCardapioViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O nome do prato é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 999.99, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal PrecoBase { get; set; }

        [Required(ErrorMessage = "O período (Almoço/Jantar) é obrigatório.")]
        public int Periodo { get; set; }

        public bool IsAtivo { get; set; }

        // Adicionada obrigatoriedade para a Imagem
        [Url(ErrorMessage = "Insira uma URL válida para a imagem.")]
        public string ImagemUrl { get; set; }

        public List<int> IngredientesIds { get; set; } = new List<int>();
    }
}