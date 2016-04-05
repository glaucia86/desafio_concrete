using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CS.DesafioGlaucia.WebApi.Entities;
using CS.DesafioGlaucia.WebApi.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace CS.DesafioGlaucia.WebApi.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //ValidateClientAuthentication
        /* Ese método irá validar o cliente. E retornará se a validação foi
         * realizada com sucesso */

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var clientId = string.Empty;
            var clientSecret = string.Empty;
            Cliente client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.Validated();
                return Task.FromResult<object>(null);
            }

            using (var repository = new AuthRepository())
            {
                client = repository.EncontrarCliente(context.ClientId);
            }

            if (client == null)
            {
                context.SetError("invalid_clientId",
                    string.Format("Cliente '{0}' não está registrado no sistema", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "O segredo do cliente deve ser enviado.");
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Segredo do cliente é inválido.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Cliente está inativo");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();

            return Task.FromResult<object>(null);
        }

        //GrantResourceOwnerCredentials
        /* Esse método irá validar o "usuario" e a "senha". Onde irá realizar a chamada do método: "EncontrarUsuario"
         * assim, verificará se o usuário e a senha são válidos. */

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});

            using (var repository = new AuthRepository())
            {
                var user = await repository.EncontrarUsuario(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "O usuário ou a senha estão incorretos");
                    return;
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "usuario"));
            identity.AddClaim(new Claim("sub", context.UserName));

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                },
                {
                    "usuario", context.UserName
                }
            });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var clienteOriginal = context.Ticket.Properties.Dictionary["as:client_id"];
            var clienteAtual = context.ClientId;

            if (clienteOriginal != clienteAtual)
            {
                context.SetError("invalid_clientId", "O Token Atualizado é diferente do emitido para o clienteId");
                return Task.FromResult<object>(null);
            }

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(n => n.Type == "newClaim").FirstOrDefault();

            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }

            newIdentity.AddClaim(new Claim("newIdentity", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var propriedade in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(propriedade.Key, propriedade.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}