using bagcash.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bagcash
{
    public class BagcashContext : DbContext
    {
        private readonly IConfiguration configuration;
        public BagcashContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BagcashContext"));
        }
    }
}
