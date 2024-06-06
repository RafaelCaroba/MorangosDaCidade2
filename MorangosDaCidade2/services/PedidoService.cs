using MorangosDaCidade2.Entities;
using MorangosDaCidade2.repositories;
using System;

namespace MorangosDaCidade2.services
{
    internal class PedidoService
    {
        public PedidoRepository pedidoRepository = new PedidoRepository();

        public bool SalvarPedido(Pedido p)
        {
            if(pedidoRepository.SalvarPedido(p) > 0) return true;
            else return false;
        }


    }
}
