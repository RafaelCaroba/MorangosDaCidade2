using MorangosDaCidade2.Entities;
using MorangosDaCidade2.repositories;
using System;
using System.Threading.Tasks;

namespace MorangosDaCidade2.services
{
    internal class PedidoService
    {
        public PedidoRepository pedidoRepository = new PedidoRepository();

        public async Task<int> SalvarPedidoAsync(Pedido p)
        {
            return await pedidoRepository.SalvarPedidoAsync(p);
        }

        public async Task<bool> SalvarItemPedidoAsync(ItemPedido item, int idPedido)
        {
            if (await pedidoRepository.SalvarItemPedidoAsync(item, idPedido) > 0) return true;
            else return false;
        }
    }
}
