using System;
using System.Web.Http;
using CS.DesafioGlaucia.WebApi;
using CS.DesafioGlaucia.WebApi.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

/* Em vez de usar o Global.asax, estarei executando o projeto desde aqui!!! */

[assembly: OwinStartup(typeof (Startup))]

namespace CS.DesafioGlaucia.WebApi
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        /* Aqui estou realizando a configuração da aplicação do projeto */

        public void Configuration(IAppBuilder app)
        {
            /* Aqui estou configurando as rotas do API e também para poder conectar o Asp.NET com
             * o servidor OWIN */
            var config = new HttpConfiguration();

            ConfigurarOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigurarOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            /* Aqui irá gerar o Token */
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}