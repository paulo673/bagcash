using System;
using System.ComponentModel.DataAnnotations;

namespace bagcash.Models.ViewModels
{
    public class TransacaoViewModel
    {
        public Tipo Tipo { get; set; }

        [Display(Name = "Descrição"), Required(ErrorMessage = "Campo {0} é obrigatório!")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório!")]
        public decimal Valor { get; set; }

        [Display(Name = "Data"), Required(ErrorMessage = "Campo {0} é obrigatório!")]
        public DateTime DataDaPrimeiraParcela { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatório!")]
        public int Categoria { get; set; }

        public bool SeRepete { get; set; }

        [Display(Name = "Número de parcelas"), Required(ErrorMessage = "Campo {0} é obrigatório!")]
        public int NumeroDeParcelas { get; set; }
        public int? FaturaId { get; set; }
        public bool Fixa { get; set; }
    }
}
