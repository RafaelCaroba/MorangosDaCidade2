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
            List<Funcionario> funcionarios = funcionarioRepository.ListarFuncionarios();
            return funcionarios;
        }

        public List<Funcionario> ListarFuncionariosPorNome(string nome)
        {
            List<Funcionario> funcionarios = funcionarioRepository.BuscarFuncionarioPorNome(nome);
            return funcionarios;
        }

        public Funcionario BuscarFuncionarioPorId(int id)
        {
            Funcionario funcionario = funcionarioRepository.BuscarFuncionarioPorId(id);
            return funcionario;
        }

        public bool AtualizarFuncionario(Funcionario f)
        {
            if (funcionarioRepository.AtualizarFuncionario(f) > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeletarFuncionario(int id)
        {
            if (funcionarioRepository.DeletarFuncionario(id) > 0)
            {
                return true;
            }
            return false;
        }
    }
}