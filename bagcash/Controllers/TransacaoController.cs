using bagcash.Models;
using bagcash.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
             .Include(x => x.Parcelas)
             .Where(x => x.Parcelas
             .Any(y => y.DataDeEfetivacao.Month == DateTime.Now.Month && y.DataDeEfetivacao.Year == DateTime.Now.Year && x.FaturaId == null))
             .AsNoTracking()
             .ToListAsync();

            return View(transacoes);
        }

        public IActionResult Cadastrar(int? faturaId)
        {
            if (faturaId != null || faturaId != 0)
            {
                ViewBag.FaturaId = faturaId;
            }

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

            var transacao = new Transacao(transacaoVm.Tipo, transacaoVm.Descricao, transacaoVm.Valor, transacaoVm.DataDeCadastro, transacaoVm.Categoria, transacaoVm.NumeroDeParcelas, transacaoVm.DataDaPrimeiraParcela, transacaoVm.FaturaId);

            context.Add(transacao);

            await context.SaveChangesAsync();

            TempData["sucesso"] = $"{transacaoVm.Tipo} cadastrada com sucesso!";
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

            await context.SaveChangesAsync();


            TempData["sucesso"] = "Transacação efetivada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PlanejamentoAnual()
        {
            var transacoes = await context.Transacoes
              .Include(x => x.Categoria)
              .Include(x => x.Parcelas)
              .AsNoTracking()
              .ToListAsync();

            return View(transacoes);
        }
    }
}