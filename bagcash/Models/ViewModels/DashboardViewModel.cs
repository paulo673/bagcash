namespace bagcash.Models.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TotalDeReceitas { get; set; }
        public decimal TotalDeDespesas { get; set; }
        public decimal Saldo => TotalDeReceitas - TotalDeDespesas;
    }
}
