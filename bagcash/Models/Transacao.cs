using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace bagcash.Models
{
    public class Transacao
    {
        protected Transacao() { }
        public Transacao(IdentityUser usuario, Tipo tipo, string descricao, decimal valor, int categoriaId, int numeroDeParcelas, DateTime dataDaPrimeiraParcela, bool fixa, int? faturaId, int id = 0)
        {
            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            Valor = valor;
            DataDeCadastro = DateTime.Now;
            CategoriaId = categoriaId;
            FaturaId = faturaId;
            Fixa = fixa;
            Usuario = usuario;
            CriarParcelas(numeroDeParcelas, dataDaPrimeiraParcela);
        }

        public int Id { get; private set; }
        public Tipo Tipo { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public bool Fixa { get; private set; }
        public DateTime DataDeCadastro { get; private set; }
        public int CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; }
        public int? FaturaId { get; private set; }
        public Fatura Fatura { get; private set; }
        public string UsuarioId { get; private set; }
        public IdentityUser Usuario { get; private set; }
        public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();

        private void CriarParcelas(int numeroDeParcelas, DateTime dataDaPrimeiraParcela)
        {
            for (int i = 1; i <= numeroDeParcelas; i++)
            {
                bool primeiraParcela = i == 1;

                if (primeiraParcela)
                {
                    this.Parcelas.Add(new Parcela(i, dataDaPrimeiraParcela));

                }
                else
                {
                    this.Parcelas.Add(new Parcela(i, dataDaPrimeiraParcela.AddMonths(i - 1)));
                }
            }
        }
    }
}
