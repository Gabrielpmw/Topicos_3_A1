using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace restaurante.Models
{
    public class ItemCardapio
    {
        public enum PeriodoCardapio
        {
            Almoco,
            Jantar
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        [Required]
        public decimal PrecoBase { get; set; }

        [Required]
        public PeriodoCardapio Periodo { get; set; }

        public ICollection<Ingrediente> Ingredientes { get; set; }

        [Required]
        public bool IsAtivo { get; set; } = true;

        public string ImagemUrl { get; set; }
    }
}