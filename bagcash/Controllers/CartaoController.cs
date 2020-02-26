using bagcash.Models;
using System.Threading.Tasks;
using bagcash.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace bagcash.Controllers
{
    [Authorize]
    public class CartaoController : Controller
    {
        private readonly BagcashContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartaoController(BagcashContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var cartoes = await context.Cartoes
                .Where(x => x.UsuarioId == usuario.Id)
                .AsNoTracking()
                .ToListAsync();

            return View(cartoes);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CartaoViewModel cartaoVm)
        {
            if (!ModelState.IsValid)
            {
                TempData["erro"] = "Oops, algo deu errado. Tente novamente!";
                return View(cartaoVm);
            }
            var usuario = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var cartao = new Cartao(usuario, cartaoVm.Nome, cartaoVm.Limite, cartaoVm.DiaDeFechamento, cartaoVm.DiaDeVencimento);

            context.Add(cartao);

            await context.SaveChangesAsync();

            TempData["sucesso"] = "Cartão cadastrado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FaturaAberta(int cartaoId)
        {
            var cartao = await context.Cartoes
                .FirstOrDefaultAsync(x => x.Id == cartaoId);

            var faturaAberta = await context.Faturas
                .Include(x => x.Cartao)
                .Include(x => x.Transacoes)
                    .ThenInclude(x => x.Categoria)
                .Include(x => x.Transacoes)
                    .ThenInclude(x => x.Parcelas)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CartaoId == cartaoId && !x.Fechada);

            if (faturaAberta == null)
            {
                var novaFatura = cartao.AbrirFatura();

                context.Add(novaFatura);

                await context.SaveChangesAsync();

                return View(novaFatura);
            }

            return View(faturaAberta);
        }
    }
}