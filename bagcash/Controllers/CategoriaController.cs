using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bagcash.Models;
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

        public async Task<IActionResult> ObterCategoriasPorTipo(string tipo)
        {
            var categorias = await context.Categorias.Where(x => x.Tipo == Enum.Parse<Tipo>(tipo)).ToListAsync();

            return Json(categorias);
        }
    }
}