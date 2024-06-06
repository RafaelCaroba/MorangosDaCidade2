using MorangosDaCidade.Controllers;
using MorangosDaCidade.Entities;
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
        public ProdutoController produtoController = new ProdutoController();
        public FuncionarioController funcionarioController = new FuncionarioController();
        public ClienteContoller clienteContoller = new ClienteContoller();

        public override void Executar()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                base.Executar();
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

        public Pedido FormularioDePedido()
        {
            ExibirTituloDaOpcao("NOVO PEDIDO");
            Console.Write("Qual o Cliente destinatário?: ");
            Console.WriteLine("1 - Buscar por Nome");
            Console.WriteLine("2 - Buscar na Lista");
            Console.WriteLine("3 - Cadastrar Novo Cliente");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());
            while (opcao < 1 && opcao > 3)
            {
                Console.WriteLine("Opção inválida");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());
            }
            Cliente cliente = new Cliente();
            switch (opcao)
            {
                case 1:
                    Console.Write("Digite o nome: ");
                    string nome = Console.ReadLine();
                    funcionarioController.ListarFuncionariosPorNome(nome);
                    Console.Write("\nDigite o Id do cliente: ");
                    int id = int.Parse(Console.ReadLine());
                    //cliente = clienteController.BuscarClientePorId(id);  
                    break;

                case 2:
                    funcionarioController.ListarFuncionarios();
                    Console.Write("\nDigite o Id do cliente: ");
                    id = int.Parse(Console.ReadLine());
                    //cliente = clienteController.BuscarClientePorId(id);
                    break;

                case 3:
                    //cliente = CadastrarCliente() 
                    break;
            }
            Console.WriteLine("Cliente adicionado ao pedido.");
            Thread.Sleep(1500);
            Console.Clear();

            List<ItemPedido> carrinho = new List<ItemPedido>();
            Console.Write("Como deseja selecionar o produto?: ");
            Console.WriteLine("1 - Buscar por Nome");
            Console.WriteLine("2 - Buscar na Lista");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());
            while (opcao < 1 && opcao > 3)
            {
                Console.WriteLine("Opção inválida");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());
            }
            produtoController.ListarProdutos();

            
            Pedido pedido = new Pedido(cliente, Status.Aberto, null);
            return pedido;
        }
    }
}
