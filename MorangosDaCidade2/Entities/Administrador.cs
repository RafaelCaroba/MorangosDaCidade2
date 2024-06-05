using System;
namespace MorangosDaCidade.Entities
{
  
    class Administrador : Usuario
    {
        public Administrador(string nome, string cpf, string email, string telefone, 
            DateTime dataNascimento, string senha) : 
            base(nome, cpf, email, telefone, dataNascimento, senha)
        {
        }

        public Administrador()
        {

        }
    }
}