﻿using System;

namespace bagcash.Models.ViewModels
{
    public class TransacaoViewModel
    {
        public Tipo Tipo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public DateTime DataDaPrimeiraParcela { get; set; }
        public int Categoria { get; set; }
        public bool SeRepete { get; set; }
        public int NumeroDeParcelas { get; set; }
        public int? FaturaId { get; set; }
    }
}