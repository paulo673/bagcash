using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace bagcash.Models
{
    public class Cartao
    {
        protected Cartao() { }
        public Cartao(IdentityUser usuario, string nome, decimal limite, int diaDeFechamento, int diaDeVencimento, int id = 0)
        {
            Id = id;
            Nome = nome;
            Limite = limite;
            DiaDeFechamento = diaDeFechamento;
            DiaDeVencimento = diaDeVencimento;
            Usuario = usuario;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Limite { get; private set; }
        public int DiaDeFechamento { get; private set; }
        public int DiaDeVencimento { get; private set; }
        public string UsuarioId { get; private set; }
        public IdentityUser Usuario { get; private set; }
        public ICollection<Fatura> Faturas { get; private set; }

        public Fatura AbrirFatura()
        {
            var proximoMes = DateTime.Now.AddMonths(1).Month;

            var diaDeFechamento = new DateTime(DateTime.Now.Year, proximoMes, this.DiaDeFechamento);
            var diaDeVencimento = new DateTime(DateTime.Now.Year, proximoMes, this.DiaDeVencimento);

            var novaFatura = new Fatura(diaDeFechamento, diaDeVencimento, this);

            return novaFatura;
        }
    }
}
