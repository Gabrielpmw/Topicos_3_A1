namespace restaurante.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public bool IsAtivo { get; set; }
        public Usuario Usuario { get; set; }
    }
}
