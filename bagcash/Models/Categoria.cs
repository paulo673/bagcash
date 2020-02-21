using System.Collections.Generic;

namespace bagcash.Models
{
    public class Categoria
    {
        public Categoria(string nome, Tipo tipo, decimal limite, int? categoriaPaiId, int id = 0)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
            Limite = limite;
            CategoriaPaiId = categoriaPaiId;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public Tipo Tipo { get; private set; }
        public decimal Limite { get; private set; }
        public int? CategoriaPaiId { get; set; }
        public Categoria CategoriaPai { get; set; }
        public ICollection<Categoria> SubCategorias { get; set; }
        public ICollection<Transacao> Transacoes { get; set; }
    }
}