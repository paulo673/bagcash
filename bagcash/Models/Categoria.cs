using System.Collections.Generic;

namespace bagcash.Models
{
    public class Categoria
    {
        public Categoria(string nome, Tipo tipo, int id = 0)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public Tipo Tipo { get; private set; }
        public ICollection<Transacao> Transacoes { get; set; }
    }
}