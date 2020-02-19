using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bagcash.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using bagcash.Models.ViewModels;

namespace bagcash.Controllers
{
    public class HomeController : Controller
    {
        private readonly BagcashContext context;

        public HomeController(BagcashContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var transacoes = await context.Transacoes.ToListAsync();

            var dashboardVm = new DashboardViewModel
            {
                TotalDeReceitas = transacoes.Where(x => x.Tipo == Tipo.Receita).Sum(x => x.Valor),
                TotalDeDespesas = transacoes.Where(x => x.Tipo == Tipo.Despesa).Sum(x => x.Valor),
            };

            return View(dashboardVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
