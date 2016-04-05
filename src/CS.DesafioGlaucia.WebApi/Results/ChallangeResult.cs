using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CS.DesafioGlaucia.WebApi.Results
{
    public class ChallangeResult : IHttpActionResult
    {
        public ChallangeResult(string loginProvider, ApiController controller)
        {
            loginProvider = loginProvider;
            Request = controller.Request;
        }

        public string LoginProvider { get; set; }

        public HttpRequestMessage Request { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            Request.GetOwinContext().Authentication.Challenge(LoginProvider);

            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }
    }
}