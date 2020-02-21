using System;

namespace bagcash.Models
{
    public class Parcela
    {
        protected Parcela() { }
        public Parcela(int numeroDaParcela, DateTime dataDeEfetivacao, bool efetivada = false, int id = 0)
        {
            Id = id;
            NumeroDaParcela = numeroDaParcela;
            DataDeEfetivacao = dataDeEfetivacao;
            Efetivada = efetivada;
        }

        public int Id { get; private set; }
        public int NumeroDaParcela { get; set; }
        public DateTime DataDeEfetivacao { get; private set; }
        public bool Efetivada { get; private set; }
    }
}