using MorangosDaCidade.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorangosDaCidade2.Entities
{
    internal class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public SqlDateTime DataPedido { get; set; }
        public Status StatusPedido { get; set; }
        public List<ItemPedido> Itens {  get; set; }

        public Pedido(Cliente cliente, SqlDateTime dataPedido, Status statusPedido, List<ItemPedido> itens)
        {
            Cliente = cliente;
            DataPedido = dataPedido;
            StatusPedido = statusPedido;
            Itens = itens;
        }

        public Pedido(int id, Cliente cliente, SqlDateTime dataPedido, Status statusPedido, List<ItemPedido> itens)
        {
            Id = id;
            Cliente = cliente;
            DataPedido = dataPedido;
            StatusPedido = statusPedido;
            Itens = itens;
        }

        public Pedido(Cliente cliente, Status statusPedido, List<ItemPedido> itens)
        {
            Cliente = cliente;
            StatusPedido = statusPedido;
            Itens = itens;
        }

        public Pedido()
        {
        }
    }
}
