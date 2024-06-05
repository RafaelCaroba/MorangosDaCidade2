using System;
using System.Collections.Generic;
using MorangosDaCidade.Entities;
using MorangosDaCidade.Repository;
namespace MorangosDaCidade.Service
{
    class FuncionarioService
    {
        public FuncionarioRepository funcionarioRepository = new FuncionarioRepository();
        public FuncionarioService() { }

        public bool SalvarFuncionario(Funcionario f)
        {
            
            if (funcionarioRepository.CadastrarFuncionario(f) > 0)
            {
                return true;
            }
            return false;
        }

        public List<Funcionario> ListarFuncionarios()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            funcionarios = funcionarioRepository.ListarFuncionarios();
            foreach (var item in funcionarios)
            {
                Console.WriteLine($"{item.Id} - {item.Nome}"); 
            }
            return funcionarios;
        }
    }
}