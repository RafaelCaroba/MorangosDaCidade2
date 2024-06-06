using System;
using System.Data.SqlTypes;
namespace MorangosDaCidade.Entities
{
    class Cliente : Usuario
    {
        public Cliente(string nome, string cpf, string email, string telefone, 
            SqlDateTime dataNascimento, string senha) : 
            base(nome, cpf, email, telefone, dataNascimento, senha)
        {
        }

        public Cliente(int id, string nome, string cpf, string email, string telefone,
            SqlDateTime dataNascimento, string senha) :
            base(id, nome, cpf, email, telefone, dataNascimento, senha)
        {
        }

        public Cliente()
        {

        }
    }
}