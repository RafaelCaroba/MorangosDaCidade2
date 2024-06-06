using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorangosDaCidade2.Entities
{
    internal class ItemPedido
    {
        public Produto Produto {  get; set; }
        public int Quantidade {  get; set; }

        public ItemPedido(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public ItemPedido() { }
    }
}
