using System.Collections.Generic;

namespace bagcash.Models.ViewModels
{
    public class PlanejamentoAnualViewModel
    {
        public IDictionary<int, decimal> TotalDeReceitasPorMes { get; set; }

        public IDictionary<int, decimal> TotalDeDespesasPorMes { get; set; }
        
        public IDictionary<int, decimal> SaldoPorMes { get; set; }
        
        public List<TotalPorCategoriaEMesDto> TotalPorMesECategoria { get; set; } = new List<TotalPorCategoriaEMesDto>();
    }
}
