﻿using System;
using System.Data.Entity;
using System.Web.Http;
using CS.DesafioGlaucia.WebApi;
using CS.DesafioGlaucia.WebApi.Providers;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

/* Em vez de usar o Global.asax, estarei executando o projeto desde aqui!!! */
[assembly: OwinStartup(typeof(Startup))]

namespace CS.DesafioGlaucia.WebApi
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }

        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }

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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, CS.DesafioGlaucia.WebApi.Migrations.Configuration>());
        }


        public void ConfigurarOAuth(IAppBuilder app)
        {
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var OAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            /* Geração do Token */
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            /* Configuração com Login Externo através do Google */
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "xxxxxx",
                ClientSecret = "xxxxxx",
                Provider = new GoogleAuthProvider()
            };

            app.UseGoogleAuthentication(googleAuthOptions);

            /* Configuração com Login Externo através do Facebok */
            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "xxxxxx",
                AppSecret = "xxxxxx",
                Provider = new FacebookAuthProvider()
            };

            app.UseFacebookAuthentication(facebookAuthOptions);
        }
    }
}