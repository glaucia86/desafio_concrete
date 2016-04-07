using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using CS.DesafioGlaucia.WebApi.Entities;

namespace CS.DesafioGlaucia.WebApi.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(AuthContext context)
        {
            if (context.Clients.Count() > 0)
            {
                return;
            }

            context.Clients.AddRange(CriarListaClientes());
            context.SaveChanges();
        }


        private static List<Cliente> CriarListaClientes()
        {
            var listaClientes = new List<Cliente>()
            {
                new Cliente
                { 
                    ClientId = "ngAuthApp", 
                    Secret = Helper.GetHash("teste@1234"), 
                    Name = "Desafio Concrete .NET", 
                    ApplicationType =  Models.ApplicationTypes.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = "https://glauthenticationdesafioconcrete.azurewebsites.net"
                },

                new Cliente
                { 
                    ClientId = "consoleApp", 
                    Secret = Helper.GetHash("123@abc"), 
                    Name = "Console Application", 
                    ApplicationType = Models.ApplicationTypes.NativeConfidential, 
                    Active = true, 
                    RefreshTokenLifeTime = 14400, 
                    AllowedOrigin = "*"
                }
            };

            return listaClientes;
        }
    }
}
