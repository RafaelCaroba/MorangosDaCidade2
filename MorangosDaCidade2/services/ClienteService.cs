using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MorangosDaCidade.Entities;
using MorangosDaCidade.Repository;
namespace MorangosDaCidade.Service
{
    class ClienteService
    {
        public ClienteRepository clienteRepository = new ClienteRepository();
        public ClienteService() { }

        public async Task<bool> SalvarCliente(Cliente f)
        {

            if (await clienteRepository.CadastrarClienteAsync(f) > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Cliente>> ListarClientes()
        {
            List<Cliente> clientes = await clienteRepository.ListarClientesAsync();
            return clientes;
        }

        public async Task<List<Cliente>> ListarClientesPorNome(string nome)
        {
            List<Cliente> clientes = await clienteRepository.BuscarClientePorNomeAsync(nome);
            return clientes;
        }

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