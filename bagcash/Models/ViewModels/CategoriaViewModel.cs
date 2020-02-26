using System.ComponentModel.DataAnnotations;

namespace bagcash.Models.ViewModels
{
    public class CategoriaViewModel
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório!")]
        public string Nome { get; set; }

        [Required]
        public Tipo TipoDeCategoria { get; set; }

        [Display(Name = "Categoria Pai")]
        public int? CategoriaPaiId { get; set; }

        [Required]
        public decimal Limite { get; set; }
    }
}
