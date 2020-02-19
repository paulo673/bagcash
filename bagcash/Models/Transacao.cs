using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bagcash.Models
{
    public class Transacao
    {
        protected Transacao() { }
        public Transacao(Tipo tipo, string descricao, decimal valor, DateTime dataDeCadastro, DateTime dataDeEfetivacao, int categoriaId, bool efetivada = false, int id = 0)
        {
            Id = id;
            Tipo = tipo;
            Descricao = descricao;
            Valor = valor;
            DataDeCadastro = dataDeCadastro;
            DataDeEfetivacao = dataDeEfetivacao;
            CategoriaId = categoriaId;
            Efetivada = efetivada;
        }

        public int Id { get; private set; }
        public Tipo Tipo { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataDeCadastro { get; private set; }
        public DateTime DataDeEfetivacao { get; private set; }
        public bool Efetivada { get; private set; }
        public int CategoriaId { get; private set; }
        public Categoria Categoria { get; set; }

        public void Efetivar()
        {
            this.Efetivada = true;
        }
    }
}
