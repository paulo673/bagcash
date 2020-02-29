using System.Threading.Tasks;
using bagcash.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bagcash.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public UsuarioController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioViewModel usuarioVm)
        {
            if (!ModelState.IsValid)
            {
                TempData["erro"] = "Oops, algo deu errado, tente novamente!";
                return View(usuarioVm);
            }

            var usuario = new IdentityUser
            {
                Email = usuarioVm.Email,
                UserName = usuarioVm.Nome,
            };

            var identityResult = await userManager.CreateAsync(usuario, usuarioVm.Senha);

            if (!identityResult.Succeeded)
            {
                TempData["erro"] = "Oops, algo deu errado, tente novamente!";
                return View(usuarioVm);
            }

            //sucesso
            return RedirectToAction("Entrar", "Autenticacao");

        }

        //public async Task<IdentityResult> AtualizarUsuario(UsuarioIdentity usuarioIdentity)
        //{
        //    return await UsuarioIdentityContexto.UpdateAsync(usuarioIdentity);
        //}

        //public async Task<IdentityResult> TrocarSenha(UsuarioIdentity usuarioIdentity, string senhaAntiga, string novaSenha)
        //{
        //    return await UsuarioIdentityContexto.ChangePasswordAsync(usuarioIdentity, senhaAntiga, novaSenha);
        //}
    }
}