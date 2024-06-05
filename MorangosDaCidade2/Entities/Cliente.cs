using System;
namespace MorangosDaCidade.Entities
{
    class Cliente : Usuario
    {
        public Cliente(string nome, string cpf, string email, string telefone, 
            DateTime dataNascimento, string senha) : 
            base(nome, cpf, email, telefone, dataNascimento, senha)
        {
        }

        public Cliente()
        {

        }
    }
}