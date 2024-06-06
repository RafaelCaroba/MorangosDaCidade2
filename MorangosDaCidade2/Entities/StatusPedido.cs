using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorangosDaCidade2.Entities
{
    public enum Status
    {
        Aberto,
        Finalizado,
        Cancelado,
    }
    internal static class StatusPedido
    {
        private static Dictionary<Status, string> descricaoStatus = new Dictionary<Status, string>
        {
            { Status.Aberto, "Pedido em Aberto" },
            { Status.Finalizado, "Pedido Finalizado" },
            { Status.Cancelado, "Pedido Cancelado" }
        };

        public static string getStatus(this Status status)
        {
            return descricaoStatus[status];
        }

    }
    
}
