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
        public Usuario Usuario { get; set; }
        public SqlDateTime DataPedido { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public List<ItemPedido> Itens {  get; set; }
    }
}
