namespace restaurante.Models
{
    public class Usuario
    {
        public enum TipoPerfil
        {
            Comum,
            Admin
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string NomeUsuario { get; set; }
        public string SenhaHash { get; set; }
        public TipoPerfil Perfil { get; set; }
        public bool IsAtivo { get; set; }
    }
}
