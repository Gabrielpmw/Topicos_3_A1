namespace restaurante.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public string CEP { get; set; }
        public string Quadra { get; set; }
        public string Alameda { get; set; }
        public string Lote { get; set; }
        public string Complemento { get; set; }
        public string Referencia { get; set; }

        public bool IsAtivo { get; set; }
        public Usuario Usuario { get; set; }
    }
}
