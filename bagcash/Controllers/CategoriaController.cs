using System;
using System.Linq;
using System.Threading.Tasks;
using bagcash.Models;
using bagcash.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bagcash.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly BagcashContext context;

        public CategoriaController(BagcashContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await context.Categorias
                .Include(x => x.CategoriaPai)
                .AsNoTracking()
                .ToListAsync();

            return View(categorias);
        }

        public async Task<IActionResult> IndexJson()
        {
            var categorias = await context.Categorias
                .Include(x => x.SubCategorias)
                .AsNoTracking()
                .ToListAsync();

            return new ObjectResult(categorias);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CategoriaViewModel categoriaVm)
        {
            if (!ModelState.IsValid)
            {
                TempData["erro"] = "Oops, algo deu errado. Tente novamente!";
                return View(categoriaVm);
            }

            var categoria = new Categoria(categoriaVm.Nome, categoriaVm.Tipo, categoriaVm.Limite, categoriaVm.CategoriaPaiId);

            context.Add(categoria);

            await context.SaveChangesAsync();

            TempData["sucesso"] = "Categoria cadastrada com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ObterCategoriasPorTipo(string tipo)
        {
            var categorias = await context.Categorias.Where(x => x.Tipo == Enum.Parse<Tipo>(tipo)).ToListAsync();

            return Json(categorias);
        }
    }
}