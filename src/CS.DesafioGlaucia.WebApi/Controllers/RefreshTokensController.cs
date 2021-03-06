﻿using System.Threading.Tasks;
using System.Web.Http;

namespace CS.DesafioGlaucia.WebApi.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {
        private AuthRepository repository = null;

        public RefreshTokensController()
        {
            repository = new AuthRepository();
        }


        [Authorize(Users = "Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(repository.SelecionarTodosRefreshToken());
        }


        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Remover(string tokenId)
        {
            var resultado = await repository.RemoverRefreshToken(tokenId);

            if (resultado)
            {
                return Ok();
            }

            return BadRequest("O Token Id não existe");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}