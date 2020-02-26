using bagcash.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bagcash
{
    public class BagcashContext : IdentityDbContext
    {
        private readonly IConfiguration configuration;
        public BagcashContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Fatura> Faturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                property.AsProperty().Builder.HasMaxLength(200, ConfigurationSource.Convention);
                property.Relational().ColumnType = "varchar(200)";
            }

            modelBuilder.Entity<Transacao>()
                .Property(x => x.Tipo)
                .HasConversion(x => x.ToString(), x => (Tipo)Enum.Parse(typeof(Tipo), x));

            modelBuilder.Entity<Categoria>()
              .Property(x => x.Tipo)
              .HasConversion(x => x.ToString(), x => (Tipo)Enum.Parse(typeof(Tipo), x));

            modelBuilder.Entity<Parcela>()
                .ToTable("Parcelas");

            modelBuilder.Entity<Fatura>()
              .ToTable("Faturas");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BagcashContext"));
        }
    }
}
