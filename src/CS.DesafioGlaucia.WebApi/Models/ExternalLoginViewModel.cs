using System.ComponentModel.DataAnnotations;

namespace CS.DesafioGlaucia.WebApi.Models
{
    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string ExternalAccessToken { get; set; }
    }

    public class ParseExternalAccessToken
    {
        public string userId { get; set; }
        public string appId { get; set; }
    }
}