using System.Data.Entity;
using CS.DesafioGlaucia.WebApi.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CS.DesafioGlaucia.WebApi
{
    /* Aqui estou realizando a conexão com a base de dados através do EF. Nota que 
     * a herança realizanda lembra o DBContext. Mas, como estou trabalhando com o Identity, estou
     usando as propriedades relacionadas ao Identity */
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
            //Default
        }

        public DbSet<Cliente> Clients { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}