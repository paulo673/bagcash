using System;
using System.Collections.Generic;

namespace bagcash.Models
{
    public class Fatura
    {
        protected Fatura() { }
        public Fatura(DateTime dataDeFechamento, DateTime dataDeVencimento, Cartao cartao)
        {
            DataDeFechamento = dataDeFechamento;
            DataDeVencimento = dataDeVencimento;
            Cartao = cartao;
        }

        public int Id { get; private set; }
        public DateTime DataDeFechamento { get; private set; }
        public DateTime DataDeVencimento { get; private set; }
        public bool Fechada { get; private set; }
        public int CartaoId { get; private set; }
        public Cartao Cartao { get; private set; }
        public ICollection<Transacao> Transacoes { get; private set; }
    }
}
