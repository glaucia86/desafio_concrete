﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS.DesafioGlaucia.WebApi.Entities;
using CS.DesafioGlaucia.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CS.DesafioGlaucia.WebApi
{
/* Aqui estou definindo a parte lógica com respeito ao usuário. A propriedade "UserManager" será o responsável para poder criptografar a senha, como e quando validar o
usuário e para gerenciar os claims da aplicação */
    public class AuthRepository : IDisposable
    {
        private AuthContext context;
        private UserManager<IdentityUser> userManager;

        public AuthRepository()
        {
            context = new AuthContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }


        public async Task<IdentityResult> RegistrarUsuario(UsuarioModel usuarioModel)
        {
            var user = new IdentityUser
            {
                UserName = usuarioModel.Usuario
            };

            var resultado = await userManager.CreateAsync(user, usuarioModel.Senha);

            return resultado;
        }


        public async Task<IdentityUser> EncontrarUsuario(string usuario, string senha)
        {
            IdentityUser user = await userManager.FindAsync(usuario, senha);

            return user;
        }


        public Cliente EncontrarCliente(string clientId)
        {
            var cliente = context.Clients.Find(clientId);

            return cliente;
        }


        public async Task<bool> AdicionarRefreshToken(RefreshToken token)
        {
            var tokenExistente = context.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId)
                    .SingleOrDefault();

            if (tokenExistente != null)
            {
                var resultado = await RemoverRefreshToken(tokenExistente);
            }

            context.RefreshTokens.Add(token);

            return await context.SaveChangesAsync() > 0;
        }


        public async Task<bool> RemoverRefreshToken(string refreshTokenId)
        {
            var refreshToken = await context.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                context.RefreshTokens.Remove(refreshToken);

                return await context.SaveChangesAsync() > 0;
            }

            return false;
        }


        public async Task<bool> RemoverRefreshToken(RefreshToken refreshToken)
        {
            context.RefreshTokens.Remove(refreshToken);

            return await context.SaveChangesAsync() > 0;
        }


        public async Task<RefreshToken> EncontrarRefreshToken(string refreshTokenId)
        {
            var refreshToken = await context.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }


        public List<RefreshToken> SelecionarTodosRefreshToken()
        {
            return context.RefreshTokens.ToList();
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await userManager.FindAsync(loginInfo);

            return user;
        }


        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var resultado = await userManager.CreateAsync(user);

            return resultado;
        }


        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var resultado = await userManager.AddLoginAsync(userId, login);

            return resultado;
        }


        public void Dispose()
        {
            context.Dispose();
            userManager.Dispose();
        }
    }
}