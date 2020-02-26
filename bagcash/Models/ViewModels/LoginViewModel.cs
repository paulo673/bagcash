using System.ComponentModel.DataAnnotations;

namespace bagcash.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o seu {0} para continuar.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe a sua {0} para continuar.")]
        [MinLength(6, ErrorMessage = "Senha inválida.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
