using System.ComponentModel.DataAnnotations;

namespace CS.DesafioGlaucia.WebApi.Models
{
    public class UsuarioModel
    {
        [Required]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve conter pelo menos {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required]
        [Compare("Senha", ErrorMessage = "A senha e a confirmação da senha não são as mesmas.")]
        [Display(Name = "Confirme a Senha")]
        public string ConfirmaSenha { get; set; }
    }
}