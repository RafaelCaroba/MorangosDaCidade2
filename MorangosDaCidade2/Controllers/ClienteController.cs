using System;
using System.Threading;
using System.Data.SqlTypes;
using MorangosDaCidade.Entities;
using MorangosDaCidade.Repository;
using MorangosDaCidade.Service;
using MorangosDaCidade2.Controllers;
using System.Collections.Generic;

namespace MorangosDaCidade.Controllers
{
    class ClienteController : Controller
    {
        public static ClienteService clienteService = new ClienteService();

        public override void Executar()
        {
            int opcao = -1;
            while (opcao != 0)
            {
                base.Executar();
                ExibirTituloDaOpcao("MENU DE CLIENTES");
                Console.WriteLine("1 - Cadastrar Novo Cliente");
                Console.WriteLine("2 - Listar Clientes");
                Console.WriteLine("3 - Editar Registro de Clientes");
                Console.WriteLine("4 - Deletar Clientes");
                Console.WriteLine("0 - Voltar");
                Console.Write("Esvolha uma opção: ");
                string opDigitada = Console.ReadLine();
                opcao = int.Parse(opDigitada);

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        CadastrarCliente();
                        break;
                    case 2:
                        Console.Clear();
                        ListarClientes();
                        break;
                    case 3:
                        Console.Clear();
                        EditarCliente();
                        break;
                    case 4:
                        Console.Clear();
                        DeletarCliente();
                        break;
                    case 0:
                        Console.Clear();
                        break;
                }

            }
        }

        public Cliente FormularioDeCliente()
        {
            ExibirTituloDaOpcao("CADASTRO DE CLIENTE");
            Console.Write("Insira o nome do cliente: ");
            String nome = Console.ReadLine();
            Console.Write("Insira o cpf do cliente: ");
            String cpf = Console.ReadLine();
            Console.Write("Insira o e-mail do cliente: ");
            String email = Console.ReadLine();
            Console.Write("Insira o telefone do cliente: ");
            String telefone = Console.ReadLine();
            Console.Write("Insira a data de nascimento: ");
            String dtNascimento = Console.ReadLine();
            SqlDateTime dtConvertida = DateTime.Parse(dtNascimento);
            Console.Write("Insira uma senha: ");
            String senha1 = Console.ReadLine();
            String senha2 = null;
            while (!senha1.Equals(senha2))
            {
                Console.Write("Insira a senha novamente: ");
                senha2 = Console.ReadLine();
            }
            Cliente cliente = new Cliente(nome, cpf, email, telefone, dtConvertida, senha1);
            return cliente;
        }
        public void CadastrarCliente()
        {
            Cliente cliente = FormularioDeCliente();

            if (clienteService.SalvarCliente(cliente))
            {
                string dataFormat = cliente.DataNascimento.ToString().Replace("00:00:00", "");
                Console.WriteLine("\nSucesso! Novo cliente Cadastrado:");
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"email: {cliente.Email}");
                Console.WriteLine($"CPF: {cliente.Cpf}");
                Console.WriteLine($"telefone: {cliente.Telefone}");
                Console.WriteLine($"Data de Nascimento: {dataFormat}");

            }
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void ListarClientes()
        {
            ExibirTituloDaOpcao("LISTA DE CLIENTES");
            List<Cliente> clientes = clienteService.ListarClientes();

            if (clientes.Count > 0)
            {
                Console.WriteLine($"{"Id",-5} | {"Nome",-20} | {"e-mail",-30} | {"CPF",-15} | {"Telefone",-15} |" +
                    $" {"Data Nasc.",-15}");
                foreach (Cliente f in clientes)
                {
                    string cpfFormat = $"{f.Cpf.Substring(0, 3)}.{f.Cpf.Substring(3, 3)}.{f.Cpf.Substring(6, 3)}-{f.Cpf.Substring(9, 2)}";
                    string telefoneFormat = $"({f.Telefone.Substring(0, 2)}) {f.Telefone.Substring(2, 5)}-{f.Telefone.Substring(7, 4)}";
                    string dtFormat = f.DataNascimento.ToString().Replace("00:00:00", "");
                    Console.WriteLine($"{f.Id,-5} | {f.Nome.ToUpper(),-20} | {f.Email,-30} | {cpfFormat,-15} " +
                                            $"| {telefoneFormat,-15} | {dtFormat,-15}");
                }
            }
            else
            {
                Console.WriteLine("Não Há registros para serem mostrados.");
            }
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void ListarClientesPorNome(string nome)
        {
            List<Cliente> clientes = clienteService.ListarClientesPorNome(nome);

            if (clientes.Count > 0)
            {
                ExibirTituloDaOpcao("LISTA DE CLIENTES");
                Console.WriteLine($"{"Id",-5} | {"Nome",-20} | {"e-mail",-30} | {"CPF",-15} | {"Telefone",-15} |" +
                    $" {"Data Nasc.",-15}");
                foreach (Cliente f in clientes)
                {
                    string cpfFormat = $"{f.Cpf.Substring(0, 3)}.{f.Cpf.Substring(3, 3)}.{f.Cpf.Substring(6, 3)}-{f.Cpf.Substring(9, 2)}";
                    string telefoneFormat = $"({f.Telefone.Substring(0, 2)}) {f.Telefone.Substring(2, 5)}-{f.Telefone.Substring(7, 4)}";
                    string dtFormat = f.DataNascimento.ToString().Replace("00:00:00", "");
                    Console.WriteLine($"{f.Id,-5} | {f.Nome.ToUpper(),-20} | {f.Email,-30} | {cpfFormat,-15} " +
                                            $"| {telefoneFormat,-15} | {dtFormat,-15}");
                }
            }
            else
            {
                Console.WriteLine("Não Há registros para serem mostrados.");
            }
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
        }


        public void EditarCliente()
        {
            ExibirTituloDaOpcao("EDITAR CLIENTE");
            Console.WriteLine("Como prefere buscar o cliente desejado?");
            Console.WriteLine("1 - Buscar por Nome");
            Console.WriteLine("2 - Buscar na Lista");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());
            while (opcao != 1 && opcao != 2)
            {
                Console.WriteLine("Opção inválida");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());
            }

            switch (opcao)
            {
                case 1:
                    Console.Write("Digite o nome: ");
                    string nome = Console.ReadLine();
                    ListarClientesPorNome(nome);
                    break;

                case 2:
                    ListarClientes();
                    break;
            }

            Console.Write("\nDigite o Id do cliente: ");
            int id = int.Parse(Console.ReadLine());
            Cliente cliente = clienteService.BuscarClientePorId(id);
            if (cliente != null)
            {
                Console.Write("Insira o nome do cliente: ");
                String nomeDigitado = Console.ReadLine();
                Console.Write("Insira o cpf do cliente: ");
                String cpf = Console.ReadLine();
                Console.Write("Insira o e-mail do cliente: ");
                String email = Console.ReadLine();
                Console.Write("Insira o telefone do cliente: ");
                String telefone = Console.ReadLine();
                Console.Write("Insira a data de nascimento: ");
                String dtNascimento = Console.ReadLine();
                SqlDateTime dtConvertida = DateTime.Parse(dtNascimento);
                Console.Write("Insira uma senha: ");
                String senha1 = Console.ReadLine();
                String senha2 = null;

                while (!senha1.Equals(senha2))
                {
                    Console.Write("Insira a senha novamente: ");
                    senha2 = Console.ReadLine();
                }

                cliente = new Cliente(id, nomeDigitado, cpf, email, telefone, dtConvertida, senha1);

                if (clienteService.AtualizarCliente(cliente))
                {
                    string dataFormat = cliente.DataNascimento.ToString().Replace("00:00:00", "");
                    Console.WriteLine("\nSucesso! Novo funcionário Cadastrado:");
                    Console.WriteLine($"Nome: {cliente.Nome}");
                    Console.WriteLine($"email: {cliente.Email}");
                    Console.WriteLine($"CPF: {cliente.Cpf}");
                    Console.WriteLine($"telefone: {cliente.Telefone}");
                    Console.WriteLine($"Data de Nascimento: {dataFormat}");

                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Não há nenhum funcionário com o id especificado.");
                Console.ReadKey();
            }
        }

        public void DeletarCliente()
        {
            ExibirTituloDaOpcao("DELETAR USUÁRIO");
            Console.WriteLine("Como prefere buscar o cliente desejado?");
            Console.WriteLine("1 - Buscar por Nome");
            Console.WriteLine("2 - Buscar na Lista");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());
            while (opcao != 1 && opcao != 2)
            {
                Console.WriteLine("Opção inválida");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());
            }

            switch (opcao)
            {
                case 1:
                    Console.Write("Digite o nome: ");
                    string nome = Console.ReadLine();
                    ListarClientesPorNome(nome);
                    break;

                case 2:
                    ListarClientes();
                    break;
            }

            Console.Write("\nDigite o Id do cliente: ");
            int id = int.Parse(Console.ReadLine());
            Cliente cliente = clienteService.BuscarClientePorId(id);
            if (cliente != null)
            {
                if (clienteService.DeletarCliente(id))
                {
                    Console.WriteLine("Cliente deletado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Houve um erro na deleção do cliente.");
                }
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Não há nenhum cliente com o id especificado.");
                Console.ReadKey();
            }
        }
    }
}