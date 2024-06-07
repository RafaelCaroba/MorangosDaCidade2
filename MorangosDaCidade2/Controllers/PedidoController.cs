using MorangosDaCidade.Controllers;
using MorangosDaCidade.Entities;
using MorangosDaCidade.Service;
using MorangosDaCidade2.Entities;
using MorangosDaCidade2.services;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MorangosDaCidade2.Controllers
{
    internal class PedidoController : Controller
    {
        public PedidoService pedidoService = new PedidoService();
        public ClienteService clienteService = new ClienteService();
        public ProdutoService produtoService = new ProdutoService();
        public ProdutoController produtoController = new ProdutoController();
        public FuncionarioController funcionarioController = new FuncionarioController();
        public ClienteController clienteController = new ClienteController();

        public override async Task ExecutarAsync()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                base.ExecutarAsync();
                ExibirTituloDaOpcao("MENU DE PEDIDOS");
                Console.WriteLine("1 - Cadastrar Novo Pedido");
                Console.WriteLine("2 - Listar Pedidos");
                Console.WriteLine("3 - Editar Registro de um Pedido");
                Console.WriteLine("4 - Deletar Pedido");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        await CadastrarPedido();
                        break;
                    case 2:
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    case 0:
                        Console.Clear();
                        break;
                }
            }
        }

        public async Task<Cliente> DefinirClienteAsync()
        {
            ExibirTituloDaOpcao("NOVO PEDIDO");
            Console.WriteLine("Qual o Cliente destinatário?: ");
            Console.WriteLine("1 - Buscar por Nome");
            Console.WriteLine("2 - Buscar na Lista");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());
            while (opcao < 1 || opcao > 2)
            {
                Console.WriteLine("Opção inválida");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());
            }
            Cliente cliente = new Cliente();
            Console.Clear();
            switch (opcao)
            {
                case 1:
                    Console.Write("Digite o nome: ");
                    string nome = Console.ReadLine();
                    await clienteController.ListarClientesPorNomeAsync(nome);
                    Console.Write("\nDigite o Id do cliente: ");
                    int id = int.Parse(Console.ReadLine());
                    cliente = clienteService.BuscarClientePorId(id);
                    break;

                case 2:
                    await clienteController.ListarClientesAsync();
                    Console.Write("\nDigite o Id do cliente: ");
                    id = int.Parse(Console.ReadLine());
                    cliente = clienteService.BuscarClientePorId(id);
                    break;
                case 3:
                    cliente = clienteController.FormularioDeCliente();
                     await clienteService.SalvarCliente(cliente);
                    break;
            }
            Console.WriteLine("Cliente adicionado ao pedido.");
            Thread.Sleep(1500);
            Console.Clear();
            return cliente;
        }

        public async Task<List<ItemPedido>> ConstruirCarrinhoAsync()
        {
            List<ItemPedido> carrinho = new List<ItemPedido>();

            int opcaoContinuar = -1;
            while (opcaoContinuar != 2)
            {
                Console.WriteLine("Como deseja selecionar o produto?: ");
                Console.WriteLine("1 - Buscar por Nome");
                Console.WriteLine("2 - Buscar na Lista");
                Console.Write("Escolha uma opção: ");
                int opcao = int.Parse(Console.ReadLine());
                while (opcao < 1 || opcao > 2)
                {
                    Console.WriteLine("Opção inválida");
                    Console.Write("Escolha uma opção: ");
                    opcao = int.Parse(Console.ReadLine());
                }

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Digite o nome do produto: ");
                        string nome = Console.ReadLine();
                        await produtoController.ListarProdutosPorNomeAsync(nome);
                        break;

                    case 2:
                        Console.Clear();
                        await produtoController.ListarProdutosAsync();
                        break;
                }

                Console.Write("Digite o Id do produto: ");
                int idProduto = int.Parse(Console.ReadLine());
                Produto produto = await produtoService.BuscarProdutoPorIdAsync(idProduto);

                Console.Write("Digite a quantidade: ");
                int qtdProduto = int.Parse(Console.ReadLine());

                if (qtdProduto > 0 && produto.Quantidade - qtdProduto > 0)
                {
                    ItemPedido item = new ItemPedido(produto, qtdProduto);
                    carrinho.Add(item);
                    Console.WriteLine("Produto adicionado ao carrinho!");
                }
                else
                {
                    Console.WriteLine("Quantidade indisponível.");
                }
                Console.Write("Deseja continuar adicionando Produtos ao carrinho? (1 - Sim | 2 - Não): ");
                opcaoContinuar = int.Parse(Console.ReadLine());
                while (opcaoContinuar < 1 && opcaoContinuar > 2)
                {
                    Console.WriteLine("Opção inválida. Digite novamente");
                    opcaoContinuar = int.Parse(Console.ReadLine());
                }
                
            }
            return carrinho;
        }

        public double CalcularValorTotal(List<ItemPedido> carrinho)
        {
            double valorTotal = 0;

            foreach (ItemPedido item in carrinho)
            {
                valorTotal += item.Quantidade * item.Produto.Valor;
            }
            return valorTotal;
        }

        public async Task CadastrarPedido()
        {
            Cliente cliente = await DefinirClienteAsync();
            List<ItemPedido> carrinho = await ConstruirCarrinhoAsync();
            Pedido pedido = new Pedido(cliente, Status.Aberto, carrinho);
            
            int idRetorno = await pedidoService.SalvarPedidoAsync(pedido);            

            if (idRetorno > 0)
            {
                foreach (ItemPedido item in carrinho)
                {
                    //await Console.Out.WriteLineAsync("IdProduto Controller: " + item.Produto.Id);
                    if (await pedidoService.SalvarItemPedidoAsync(item, idRetorno))
                    {
                        Console.WriteLine("\nPedido efetuado! Informações do Pedido:");
                        Console.WriteLine($"Nome do Cliente: {cliente.Nome}");
                        Console.WriteLine($"E-mail do Cliente: {cliente.Email}");
                        Console.WriteLine($"Status atual do pedido: {pedido.StatusPedido.getStatus()}");
                        double valorTotal = CalcularValorTotal(carrinho);
                        Console.WriteLine($"Valor total do pedido: R${valorTotal:N2}");
                        ExibirTituloDaOpcao("CARRINHO");
                        Console.WriteLine($"{"Produto",-15} | {"Quantidade",-20} | {"Preço",-10}");
                        foreach (ItemPedido i in carrinho)
                        {
                            Console.WriteLine($"{i.Produto.Nome,-15} | {i.Quantidade,-20} | {i.Produto.Valor,-10}");
                        }
                    }
                   else
                    {
                        Console.WriteLine("Deu ruim");
                    }
                }
                
            }
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
