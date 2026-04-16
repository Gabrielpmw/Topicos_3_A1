using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restaurante.Models;
using System.Linq;

namespace restaurante.Data
{
    public class RestauranteContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
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
        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================================
            // 1. TRAVAS DE SEGURANÇA (Índices Únicos) PARA USUÁRIOS
            // =========================================================
            // O Identity já garante que Email e UserName sejam únicos.
            // Aqui garantimos que ninguém cadastre o mesmo CPF ou Telefone.

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.CPF)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();
            // =========================================================

            modelBuilder.Entity<ItemPedido>()
                .HasKey(ip => new { ip.PedidoId, ip.ItemCardapioId });

            // Configuração de Herança (TPH) para Atendimentos
            modelBuilder.Entity<AtendimentoRetirada>();
            modelBuilder.Entity<AtendimentoDeliveryProprio>();
            modelBuilder.Entity<AtendimentoDeliveryApp>();

            // Formatação global para propriedades decimais (Dinheiro/Taxas)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
        }
    }
}