using bagcash.Models;
using bagcash.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bagcash.Controllers
{
    [Authorize]
    public class TransacaoController : Controller
    {
        private readonly BagcashContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TransacaoController(BagcashContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var usuario = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var transacoes = await context.Transacoes
             .Include(x => x.Categoria)
             .Include(x => x.Parcelas)
             .Where(x => x.UsuarioId == usuario.Id && x.Parcelas
             .Any(y => y.DataDeEfetivacao.Month == DateTime.Now.Month && y.DataDeEfetivacao.Year == DateTime.Now.Year && x.FaturaId == null))
             .AsNoTracking()
             .ToListAsync();

            return View(transacoes);
        }

        public IActionResult Cadastrar(Tipo tipoDeTransacao, int? faturaId)
        {
            if (faturaId != null || faturaId != 0)
            {
                ViewBag.FaturaId = faturaId;
            }

            ViewData["tipoDeTransacao"] = tipoDeTransacao;
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

            var usuario = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var transacao = new Transacao(usuario, transacaoVm.Tipo, transacaoVm.Descricao, transacaoVm.Valor, transacaoVm.Categoria, transacaoVm.NumeroDeParcelas, transacaoVm.DataDaPrimeiraParcela, transacaoVm.Fixa, transacaoVm.FaturaId);

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
            var usuario = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);

            var transacoes = await context.Transacoes
              .Include(x => x.Categoria)
              .Include(x => x.Parcelas)
              .Where(x => x.UsuarioId == usuario.Id)
              .AsNoTracking()
              .ToListAsync();

            int[] meses = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var totalDeReceitasDoMes = 0.0m;
            var totalDeDespesasDoMes = 0.0m;
            var saldoDoMes = 0.0m;

            IDictionary<int, decimal> totalDeReceitasPorMes = new Dictionary<int, decimal>();
            IDictionary<int, decimal> totalDeDespesasPorMes = new Dictionary<int, decimal>();
            IDictionary<int, decimal> saldoPorMes = new Dictionary<int, decimal>();

            var planejamentoAnual = new PlanejamentoAnualViewModel();

            foreach (var mes in meses)
            {
                var receitasDoMes = transacoes.Where(x => x.Tipo == Tipo.Receita && !x.Fixa && x.Parcelas.Any(y => y.DataDeEfetivacao.Month == mes && y.DataDeEfetivacao.Year == DateTime.Now.Year));
                var receitasFixas = transacoes.Where(x => x.Tipo == Tipo.Receita && x.Fixa);
                totalDeReceitasDoMes += receitasDoMes.Sum(x => x.Valor);
                totalDeReceitasDoMes += receitasFixas.Sum(x => x.Valor);

                var despesasDoMes = transacoes.Where(x => x.Tipo == Tipo.Despesa && !x.Fixa && x.Parcelas.Any(y => y.DataDeEfetivacao.Month == mes && y.DataDeEfetivacao.Year == DateTime.Now.Year));
                var despesasFixas = transacoes.Where(x => x.Tipo == Tipo.Despesa && x.Fixa);
                totalDeDespesasDoMes += despesasDoMes.Sum(x => x.Valor);
                totalDeDespesasDoMes += despesasFixas.Sum(x => x.Valor);

                totalDeReceitasPorMes.Add(mes, totalDeReceitasDoMes);
                totalDeDespesasPorMes.Add(mes, totalDeDespesasDoMes);

                saldoDoMes = totalDeReceitasDoMes - totalDeDespesasDoMes;

                saldoPorMes.Add(mes, saldoDoMes);


                totalDeReceitasDoMes = 0.0m;
                totalDeDespesasDoMes = 0.0m;
                saldoDoMes = 0.0m;
            }
            var categorias = await context.Categorias.ToListAsync();

            foreach (var categoria in categorias)
            {
                IDictionary<int, decimal> totalPorMes = new Dictionary<int, decimal>();

                foreach (var mes in meses)
                {
                    var a = transacoes.Where(x => x.CategoriaId == categoria.Id && !x.Fixa && x.Parcelas.Any(y => y.DataDeEfetivacao.Month == mes && y.DataDeEfetivacao.Year == DateTime.Now.Year)).Sum(y => y.Valor);

                    var b = transacoes.Where(x => x.CategoriaId == categoria.Id && x.Fixa).Sum(y => y.Valor);

                    totalPorMes.Add(mes, a + b);
                }

                var totalPorMesECategoria = new TotalPorCategoriaEMesDto
                {
                    Categoria = categoria.Nome,
                    Tipo = categoria.Tipo,
                    TotalPorMes = totalPorMes
                };

                planejamentoAnual.TotalPorMesECategoria.Add(totalPorMesECategoria);
            }

            planejamentoAnual.TotalDeReceitasPorMes = totalDeReceitasPorMes;
            planejamentoAnual.TotalDeDespesasPorMes = totalDeDespesasPorMes;
            planejamentoAnual.SaldoPorMes = saldoPorMes;

            return View(planejamentoAnual);
        }
    }
}