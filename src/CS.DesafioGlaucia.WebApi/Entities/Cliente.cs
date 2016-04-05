using System.ComponentModel.DataAnnotations;
using CS.DesafioGlaucia.WebApi.Models;

namespace CS.DesafioGlaucia.WebApi.Entities
{
    public class Cliente
    {
        [Key]
        public string ClientId { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ApplicationTypes ApplicationType { get; set; }

        public bool Active { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
    }
}