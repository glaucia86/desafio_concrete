using System.Collections.Generic;
using System.Web.Http;

namespace CS.DesafioGlaucia.WebApi.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(Pedido.CriarPedidos());
        }
    }

    public class Pedido
    {
        public int PedidoId { get; set; }

        public string NomeCliente { get; set; }

        public string CidadePedidoExpedido { get; set; }

        public bool Enviado { get; set; }


        public static List<Pedido> CriarPedidos()
        {
            var listaPedido = new List<Pedido>
            {
                new Pedido
                {
                    PedidoId = 1,
                    NomeCliente = "Glaucia Lemos",
                    CidadePedidoExpedido = "Rio de Janeiro",
                    Enviado = true
                },
                new Pedido
                {
                    PedidoId = 2,
                    NomeCliente = "Jurema Lemos",
                    CidadePedidoExpedido = "Minas Gerais",
                    Enviado = false
                },
                new Pedido
                {
                    PedidoId = 3,
                    NomeCliente = "José Lemos",
                    CidadePedidoExpedido = "São Paulo",
                    Enviado = false
                },
                new Pedido
                {
                    PedidoId = 4,
                    NomeCliente = "Jake Lemos",
                    CidadePedidoExpedido = "Brasília",
                    Enviado = false
                },
                new Pedido {PedidoId = 5, NomeCliente = "Sofia Lemos", CidadePedidoExpedido = "Paraná", Enviado = true}
            };

            return listaPedido;
        }
    }
}