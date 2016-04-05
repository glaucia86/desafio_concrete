using System.Threading.Tasks;
using System.Web.Http;
using CS.DesafioGlaucia.WebApi.Models;
using Microsoft.AspNet.Identity;

namespace CS.DesafioGlaucia.WebApi.Controllers
{
    public class AccountController : ApiController
    {
        private AuthRepository repository;

        public AccountController()
        {
            repository = new AuthRepository();
        }

        //POST: api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Registrar(UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var resultado = await repository.RegistrarUsuario(usuarioModel);

            IHttpActionResult errorResult = RetornarErro(resultado);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }

            base.Dispose(disposing);
        }

        /* Esse método aqui irá retornar e validar o usuarioModel */
        private IHttpActionResult RetornarErro(IdentityResult resultado)
        {
            if (resultado == null)
            {
                return InternalServerError();
            }

            if (!resultado.Succeeded)
            {
                if (resultado.Errors != null)
                {
                    foreach (string error in resultado.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest();
            }

            return null;
        }
    }
}
