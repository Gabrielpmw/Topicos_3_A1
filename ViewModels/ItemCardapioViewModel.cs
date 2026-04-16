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
        public decimal PrecoBase { get; set; }

        [Required(ErrorMessage = "O período (Almoço/Jantar) é obrigatório.")]
        public int Periodo { get; set; }

        /// <summary>
        /// Define se o prato está visível para os clientes no cardápio.
        /// Necessário para a lógica de Ativar/Desativar do painel administrativo.
        /// </summary>
        public bool IsAtivo { get; set; }

        /// <summary>
        /// Lista de IDs dos ingredientes associados ao prato para preenchimento do formulário.
        /// </summary>
        public List<int> IngredientesIds { get; set; } = new List<int>();
    }
}