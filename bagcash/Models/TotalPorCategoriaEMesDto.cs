using System.Collections.Generic;

namespace bagcash.Models
{
    public class TotalPorCategoriaEMesDto
    {
        public string Categoria { get; set; }
        public Tipo Tipo { get; set; }
        public IDictionary<int, decimal> TotalPorMes { get; set; }
    }
}
