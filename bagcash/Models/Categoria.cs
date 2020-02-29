using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace bagcash.Models
{
    public class Categoria
    {
        protected Categoria() { }
        public Categoria(IdentityUser usuario, string nome, Tipo tipo, decimal limite, int? categoriaPaiId, int id = 0)
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
        public int? CategoriaPaiId { get; private set; }
        public Categoria CategoriaPai { get; private set; }
        public string UsuarioId { get; private set; }
        public IdentityUser Usuario { get; private set; }
        public ICollection<Categoria> SubCategorias { get; set; }
        public ICollection<Transacao> Transacoes { get; set; }
    }
}