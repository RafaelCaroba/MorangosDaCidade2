using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorangosDaCidade2.Entities
{
    internal class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public bool Disponivel {  get; set; }
        public double Valor {  get; set; }


        public Produto(int id, string nome, string descricao, int quantidade, bool disponivel, double valor)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Quantidade = quantidade;
            Disponivel = disponivel;
            Valor = valor;
        }

        public Produto(string nome, string descricao, int quantidade, bool disponivel, double valor)
        {
            Nome = nome;
            Descricao = descricao;
            Quantidade = quantidade;
            Disponivel = disponivel;
            Valor = valor;
        }

        public Produto()
        {

        }
    }
}
