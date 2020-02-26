using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bagcash.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using bagcash.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace bagcash.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly BagcashContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HomeController(BagcashContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var transacoes = await context.Transacoes.ToListAsync();

            var dashboardVm = new DashboardViewModel
            {
                TotalDeReceitas = transacoes.Where(x => x.UsuarioId == usuario.Id && x.Tipo == Tipo.Receita).Sum(x => x.Valor),
                TotalDeDespesas = transacoes.Where(x => x.UsuarioId == usuario.Id && x.Tipo == Tipo.Despesa).Sum(x => x.Valor),
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
