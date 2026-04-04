using System.ComponentModel.DataAnnotations;
using static restaurante.Models.ItemCardapio;

namespace restaurante.ViewModels
{
    public class ItemCardapioViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O preço base é obrigatório.")]
        public decimal PrecoBase { get; set; }

        [Required(ErrorMessage = "O período é obrigatório.")]
        public int Periodo { get; set; }

        public List<int> IngredientesIds { get; set; } = new List<int>();
    }
}
