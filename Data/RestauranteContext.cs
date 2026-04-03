using Microsoft.EntityFrameworkCore;
using restaurante.Models;

namespace restaurante.Data
{
    public class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ItemCardapio> ItensCardapio { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<SugestaoChefe> SugestoesChefe { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Avisa o Entity Framework que a classe ItemPedido tem uma Chave Primária Composta
            modelBuilder.Entity<ItemPedido>()
                .HasKey(ip => new { ip.PedidoId, ip.ItemCardapioId });

            // APRESENTANDO AS CLASSES FILHAS PARA O BANCO DE DADOS:
            // Isso fará o EF Core criar uma única tabela "Atendimentos" com uma coluna "Discriminator"
            modelBuilder.Entity<AtendimentoRetirada>();
            modelBuilder.Entity<AtendimentoDeliveryProprio>();
            modelBuilder.Entity<AtendimentoDeliveryApp>();
        }
    }
}