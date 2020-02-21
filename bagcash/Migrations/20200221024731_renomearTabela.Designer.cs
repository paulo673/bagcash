﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bagcash;

namespace bagcash.Migrations
{
    [DbContext(typeof(BagcashContext))]
    [Migration("20200221024731_renomearTabela")]
    partial class renomearTabela
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("bagcash.Models.Cartao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DiaDeFechamento");

                    b.Property<int>("DiaDeVencimento");

                    b.Property<decimal>("Limite");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Cartoes");
                });

            modelBuilder.Entity("bagcash.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoriaPaiId");

                    b.Property<decimal>("Limite");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Tipo")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoriaPaiId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("bagcash.Models.Fatura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartaoId");

                    b.Property<DateTime>("DataDeFechamento");

                    b.Property<DateTime>("DataDeVencimento");

                    b.HasKey("Id");

                    b.HasIndex("CartaoId");

                    b.ToTable("Faturas");
                });

            modelBuilder.Entity("bagcash.Models.Parcela", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataDeEfetivacao");

                    b.Property<bool>("Efetivada");

                    b.Property<int>("NumeroDaParcela");

                    b.Property<int?>("TransacaoId");

                    b.HasKey("Id");

                    b.HasIndex("TransacaoId");

                    b.ToTable("Parcelas");
                });

            modelBuilder.Entity("bagcash.Models.Transacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriaId");

                    b.Property<DateTime>("DataDeCadastro");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<int?>("FaturaId");

                    b.Property<string>("Tipo")
                        .IsRequired();

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("FaturaId");

                    b.ToTable("Transacoes");
                });

            modelBuilder.Entity("bagcash.Models.Categoria", b =>
                {
                    b.HasOne("bagcash.Models.Categoria", "CategoriaPai")
                        .WithMany("SubCategorias")
                        .HasForeignKey("CategoriaPaiId");
                });

            modelBuilder.Entity("bagcash.Models.Fatura", b =>
                {
                    b.HasOne("bagcash.Models.Cartao", "Cartao")
                        .WithMany("Faturas")
                        .HasForeignKey("CartaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("bagcash.Models.Parcela", b =>
                {
                    b.HasOne("bagcash.Models.Transacao")
                        .WithMany("Parcelas")
                        .HasForeignKey("TransacaoId");
                });

            modelBuilder.Entity("bagcash.Models.Transacao", b =>
                {
                    b.HasOne("bagcash.Models.Categoria", "Categoria")
                        .WithMany("Transacoes")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("bagcash.Models.Fatura", "Fatura")
                        .WithMany("Transacoes")
                        .HasForeignKey("FaturaId");
                });
#pragma warning restore 612, 618
        }
    }
}
