using System;

namespace bagcash.Models.ViewModels
{
    public class CartaoViewModel
    {
        public string Nome { get; set; }
        public decimal Limite { get; set; }
        public int DiaDeFechamento { get; set; }
        public int DiaDeVencimento { get; set; }
    }
}
