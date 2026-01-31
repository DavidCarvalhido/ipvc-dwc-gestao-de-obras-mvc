using Microsoft.EntityFrameworkCore;
using MVCTrabDWC.Models;

namespace MVCTrabDWC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // Tabelas principais
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<Obra> Obras { get; set; }

        // Tabelas relacionadas (registos)
        public DbSet<RegistoMaterial> RegistosMaterial { get; set; }
        public DbSet<RegistoMaoObra> RegistosMaoObra { get; set; }
        public DbSet<RegistoPagamento> RegistosPagamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //
            // 🔧 Configurações específicas
            //

            // Cliente.NIF deve ser único
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.NIF)
                .IsUnique();

            // Converter o Enum OperacaoStock para string na base de dados
            modelBuilder.Entity<RegistoMaterial>()
                .Property(r => r.Operacao)
                .HasConversion<string>();

            // Chaves estrangeiras e relações (explicito para consistência)
            modelBuilder.Entity<Obra>()
                .HasOne(o => o.Cliente)
                .WithMany(c => c.Obras)
                .HasForeignKey(o => o.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegistoMaterial>()
                .HasOne(r => r.Obra)
                .WithMany(o => o.RegistosMaterial)
                .HasForeignKey(r => r.ObraId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RegistoMaterial>()
                .HasOne(r => r.Material)
                .WithMany(m => m.RegistosMaterial)
                .HasForeignKey(r => r.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegistoMaoObra>()
                .HasOne(r => r.Obra)
                .WithMany(o => o.RegistosMaoObra)
                .HasForeignKey(r => r.ObraId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RegistoPagamento>()
                .HasOne(r => r.Obra)
                .WithMany(o => o.RegistosPagamento)
                .HasForeignKey(r => r.ObraId)
                .OnDelete(DeleteBehavior.Cascade);

            //
            // Precisão e constraints opcionais
            //

            // Configura valores decimais para o valor de pagamento
            modelBuilder.Entity<RegistoPagamento>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2);
        }
    }
}
