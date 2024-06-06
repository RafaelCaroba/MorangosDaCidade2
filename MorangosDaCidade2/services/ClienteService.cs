using System;
using System.Collections.Generic;
using MorangosDaCidade.Entities;
using MorangosDaCidade.Repository;
namespace MorangosDaCidade.Service
{
    class ClienteService
    {
        public ClienteRepository clienteRepository = new ClienteRepository();
        public ClienteService() { }

        public bool SalvarCliente(Cliente f)
        {

            if (clienteRepository.CadastrarCliente(f) > 0)
            {
                return true;
            }
            return false;
        }

        public List<Cliente> ListarClientes()
        {
            List<Cliente> clientes = clienteRepository.ListarClientes();
            return clientes;
        }

        public List<Cliente> ListarClientesPorNome(string nome)
        {
            List<Cliente> clientes = clienteRepository.BuscarClientePorNome(nome);
            return clientes;
        }

        public bool LoginCliente(string email, string senha)
        {
            Cliente cliente = clienteRepository.LoginCliente(email, senha);

            return true;
        }
        Cliente cliente = clienteRepository.LoginCliente();

        public Cliente BuscarClientePorId(int id)
        {
            Cliente cliente = clienteRepository.BuscarClientePorId(id);
            return cliente;
        }

        public bool AtualizarCliente(Cliente f)
        {
            if (clienteRepository.AtualizarCliente(f) > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeletarCliente(int id)
        {
            if (clienteRepository.DeletarCliente(id) > 0)
            {
                return true;
            }
            return false;
        }
    }
}