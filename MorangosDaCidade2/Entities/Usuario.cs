using System;
using System.Data.SqlTypes;
namespace MorangosDaCidade.Entities
{
    class Usuario
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public String Cpf { get; set;}
        public String Email { get; set; }
        public String Telefone { get; set; }
        public SqlDateTime DataNascimento { get; set; }
        public String Senha { get; set; }

        public Usuario(string nome, string cpf, string email, string telefone, SqlDateTime dataNascimento, string senha)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            DataNascimento = dataNascimento;
            Senha = senha;
        }

        public Usuario(int id, string nome, string cpf, string email, string telefone, SqlDateTime dataNascimento, string senha)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            DataNascimento = dataNascimento;
            Senha = senha;
        }

        public Usuario ()
        {

        }


    }
}