using bagcash.Models;
using bagcash.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace bagcash.Controllers
{
    public class TransacaoController : Controller
    {
        private readonly BagcashContext context;

        public TransacaoController(BagcashContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var transacoes = await context.Transacoes
             .Include(x => x.Categoria)
             .AsNoTracking()
             .ToListAsync();

            return View(transacoes);
        }

        public async Task<IActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(TransacaoViewModel transacaoVm)
        {
            if (!ModelState.IsValid)
            {
                TempData["erro"] = "Oops, algo deu errado, tente novamente!";
                return View(transacaoVm);
            }

            var transacao = new Transacao(transacaoVm.Tipo, transacaoVm.Descricao, transacaoVm.Valor, transacaoVm.DataDeCadastro, transacaoVm.DataDeEfetivacao, transacaoVm.Categoria);

            context.Add(transacao);

            await context.SaveChangesAsync();

            TempData["sucesso"] = "Transacação cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int transacaoId)
        {
            var transacao = await context.Transacoes
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id.Equals(transacaoId));

            return View(transacao);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int transacaoId)
        {
            var transacao = await context.Transacoes
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id.Equals(transacaoId));

            context.Remove(transacao);
            await context.SaveChangesAsync();

            TempData["sucesso"] = "Transacação excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detalhes(int transacaoId)
        {
            var transacao = await context.Transacoes
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id.Equals(transacaoId));

            return View(transacao);
        }

        [HttpPost]
        public async Task<IActionResult> Efetivar(int transacaoId)
        {
            var transacao = await context.Transacoes
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id.Equals(transacaoId));

            transacao.Efetivar();

            await context.SaveChangesAsync();


            TempData["sucesso"] = "Transacação efetivada com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}