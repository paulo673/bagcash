using System.Threading.Tasks;
using bagcash.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bagcash.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public AutenticacaoController(UserManager<IdentityUser> usuarioIdentityContexto, SignInManager<IdentityUser> acessoIdentityContexto, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = usuarioIdentityContexto;
            this.signInManager = acessoIdentityContexto;
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Entrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(LoginViewModel loginVm)
        {
            var usuario = await userManager.FindByEmailAsync(loginVm.Email);

            var signInResult = await signInManager.PasswordSignInAsync(usuario, loginVm.Senha, false, true);

            if (!signInResult.Succeeded)
            {
                return View(loginVm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //public async Task<UsuarioIdentity> BuscarPorEmail(string email)
        //{
        //    return await UsuarioIdentityContexto.FindByEmailAsync(email);
        //}

        //public async Task<UsuarioIdentity> UsuarioLogado()
        //{
        //    return await UsuarioIdentityContexto.GetUserAsync(HttpContextAccessor.HttpContext.User);
        //}

        public async Task<IActionResult> Sair()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(Entrar));
        }



        //public async Task<UsuarioIdentity> BuscarPorId(string id)
        //{
        //    return await UsuarioIdentityContexto.FindByIdAsync(id);
        //}
    }
}