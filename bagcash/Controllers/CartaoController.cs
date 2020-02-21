using System.Threading.Tasks;
using bagcash.Models;
using bagcash.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bagcash.Controllers
{
    public class CartaoController : Controller
    {
        private readonly BagcashContext context;

        public CartaoController(BagcashContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cartoes = await context.Cartoes
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

            var cartao = new Cartao(cartaoVm.Nome, cartaoVm.Limite, cartaoVm.DiaDeFechamento, cartaoVm.DiaDeVencimento);

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