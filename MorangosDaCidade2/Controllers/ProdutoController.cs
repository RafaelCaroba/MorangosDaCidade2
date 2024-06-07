using MorangosDaCidade.Entities;
using MorangosDaCidade.Service;
using MorangosDaCidade2.Entities;
using MorangosDaCidade2.repositories;
using MorangosDaCidade2.services;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading;
using System.Threading.Tasks;


namespace MorangosDaCidade2.Controllers
{
    internal class ProdutoController : Controller
    {
        public static ProdutoService produtoService = new ProdutoService();

        public override async Task ExecutarAsync()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                base.ExecutarAsync();
                ExibirTituloDaOpcao("MENU DE FUNCIONÁRIO");
                Console.WriteLine("1 - Cadastrar Novo Produto");
                Console.WriteLine("2 - Listar Produtos");
                Console.WriteLine("3 - Editar Registro de um Produto");
                Console.WriteLine("4 - Deletar Produto");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        await CadastrarProdutoAsync();
                        break;
                    case 2:
                        Console.Clear();
                        await ListarProdutosAsync();
                        Console.WriteLine("Digite qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        await EditarProdutoAsync();
                        break;
                    case 4:
                        Console.Clear();
                        await DeletarProdutoAsync();
                        Console.WriteLine("Digite qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case 0:
                        Console.Clear();
                        break;
                }
            }
        }

        public Produto FormularioDeProduto()
        {
            Console.Write("Insira o nome do novo produto: ");
            String nome = Console.ReadLine();
            Console.Write("Insira a descrição do produto: ");
            String descricao = Console.ReadLine();
            Console.Write("Insira a quantidade atual do produto: ");
            int qtd = int.Parse(Console.ReadLine());
            Console.Write("O produto está disponível? (1 - Sim | 2 - Não): ");
            int disp = int.Parse(Console.ReadLine());
            bool disponivel;
            while (disp < 1 || disp > 2)
            {
                Console.WriteLine("Opção inválida");
                Console.Write("Escolha uma opção: ");
                disp = int.Parse(Console.ReadLine());
            }
            if (disp == 1) disponivel = true; else disponivel = false;
            Console.Write("Insira o valor unitário do produto: ");
            double valor = double.Parse(Console.ReadLine());

            Produto produto = new Produto(nome, descricao, qtd, disponivel, valor);
            return produto;
        }    

        public async Task CadastrarProdutoAsync()
        {
            Produto produto = FormularioDeProduto();

            if (await produtoService.SalvarProdutoAsync(produto))
            {
                Console.WriteLine("\nSucesso! Novo produto Cadastrado:");
                Console.WriteLine($"Nome do Produto: {produto.Nome}");
                Console.WriteLine($"Descrição: {produto.Descricao}");
                Console.WriteLine($"Quantidade atual: {produto.Quantidade}");
                Console.WriteLine($"O produto está disponível? {(produto.Disponivel ? "Sim" : "Não")}");
                Console.WriteLine($"Valor unitário: {produto.Valor}");
            }
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public async 
        Task
ListarProdutosAsync()
        {
            ExibirTituloDaOpcao("LISTA DE PRODUTOS");
            List<Produto> produtos = await produtoService.ListarProdutosAsync();

            if (produtos.Count > 0)
            {
                Console.WriteLine($"{"ID",-5} | {"NOME",-30} | {"QUANTIDADE",10} | {"DISPONÍVEL?",10} | {"VALOR"}");
                Console.WriteLine(new string('-', 75));
                foreach (Produto p in produtos)
                {
                    Console.WriteLine($"{p.Id,-5} | {p.Nome.ToUpper(),-30} | {p.Quantidade,10} " +
                        $"| {(p.Disponivel ? "Sim" : "Não"),10} | {p.Valor:N2} R$");
                }
            }
            else
            {
                Console.WriteLine("Não Há registros para serem mostrados.");
            }
            
        }

        public async Task ListarProdutosPorNomeAsync(string nome)
        {
            List<Produto> produtos = await produtoService.ListarProdutosPorNomeAsync(nome);

            if (produtos.Count > 0)
            {
                ExibirTituloDaOpcao("LISTA DE FUNCIONÁRIOS");
                Console.WriteLine($"{"ID",-5} | {"NOME",-20} | {"QUANTIDADE",10} | {"DISPONÍVEL?",10} | {"VALOR"}");
                foreach (Produto p in produtos)
                {
                    Console.WriteLine($"{p.Id,-5} | {p.Nome.ToUpper(),-20} | {p.Quantidade,10} " +
                        $"| {(p.Disponivel ? "Sim" : "Não"),10} | {p.Valor:N2} R$");
                }
            }
            else
            {
                Console.WriteLine("Não Há registros para serem mostrados.");
            }
            
        }

        public async Task EditarProdutoAsync()
        {
            ExibirTituloDaOpcao("EDITAR PRODUTO");
            Console.WriteLine("Como prefere buscar o Produto desejado?");
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
                    await ListarProdutosPorNomeAsync(nome);
                    break;

                case 2:
                    await ListarProdutosAsync();
                    break;
            }

            Console.Write("\nDigite o Id do produto: ");
            int id = int.Parse(Console.ReadLine());
            Produto produto = await produtoService.BuscarProdutoPorIdAsync(id);
            Thread.Sleep(2000);
            if (produto != null)
            {
                
                produto = FormularioDeProduto();
                produto.Id = id;

                if (await produtoService.AtualizarProdutoAsync(produto))
                {
                    Console.WriteLine("\nSucesso! Novo produto Cadastrado:");
                    Console.WriteLine($"Nome do Produto: {produto.Nome}");
                    Console.WriteLine($"Descrição: {produto.Descricao}");
                    Console.WriteLine($"Quantidade atual: {produto.Quantidade}");
                    Console.WriteLine($"O produto está disponível? {(produto.Disponivel ? "Sim" : "Não")}");
                    Console.WriteLine($"Valor unitário: {produto.Valor}");

                }
                Console.WriteLine("Digite qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Não há nenhum funcionário com o id especificado.");
                Console.ReadKey();
            }
        }

        public async Task DeletarProdutoAsync()
        {
            ExibirTituloDaOpcao("DELETAR PRODUTO");
            Console.WriteLine("Como prefere buscar o Produto desejado?");
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
                    await ListarProdutosPorNomeAsync(nome);
                    break;

                case 2:
                    await ListarProdutosAsync();
                    break;
            }

            Console.Write("\nDigite o Id do Produto: ");
            int id = int.Parse(Console.ReadLine());
            Produto produto = await produtoService.BuscarProdutoPorIdAsync(id);
            if (produto != null)
            {
                if (await produtoService.DeletarProdutoAsync(id)) Console.WriteLine("Produto deletado com sucesso!");
                else
                {
                    Console.WriteLine("Houve um erro na deleção do funcionário.");
                }
                
            }
            else
            {
                Console.WriteLine("Não há nenhum produto com o id especificado.");
                Console.ReadKey();
            }
        }
    }
}
