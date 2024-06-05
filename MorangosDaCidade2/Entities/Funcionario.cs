using System;
using System.Data.SqlTypes;
namespace MorangosDaCidade.Entities
{
    class Funcionario : Usuario
    {
        public Funcionario(int id, string nome, string cpf, string email, string telefone, 
            SqlDateTime dataNascimento, string senha) : 
            base(nome, cpf, email, telefone, dataNascimento, senha)
        {
        }

        public Funcionario() { }
    }
}