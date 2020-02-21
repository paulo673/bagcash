using System.ComponentModel.DataAnnotations;

namespace bagcash.Models.ViewModels
{
    public class CategoriaViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public Tipo Tipo { get; set; }

        [Display(Name = "Categoria Pai")]
        public int? CategoriaPaiId { get; set; }

        [Required]
        public decimal Limite { get; set; }
    }
}
