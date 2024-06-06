using MorangosDaCidade.Entities;
using MorangosDaCidade.Service;
using MorangosDaCidade2.Entities;
using MorangosDaCidade2.repositories;
using MorangosDaCidade2.services;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;


namespace MorangosDaCidade2.Controllers
{
    internal class ProdutoController : Controller
    {
        public static ProdutoService produtoService = new ProdutoService();

        public override void Executar()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                base.Executar();
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
                        CadastrarProduto();
                        break;
                    case 2:
                        Console.Clear();
                        ListarProdutos();
                        break;
                    case 3:
                        Console.Clear();
                        EditarProduto();
                        break;
                    case 4:
                        Console.Clear();
                        DeletarProduto();
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
            bool disponivel;
            if (Console.ReadLine().Equals("Sim")) disponivel = true; else disponivel = false;
            Console.Write("Insira o valor unitário do produto: ");
            double valor = double.Parse(Console.ReadLine());

            Produto produto = new Produto(nome, descricao, qtd, disponivel, valor);
            return produto;
        }    

        public void CadastrarProduto()
        {
            Produto produto = FormularioDeProduto();

            if (produtoService.SalvarProduto(produto))
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

        public void ListarProdutos()
        {
            ExibirTituloDaOpcao("LISTA DE PRODUTOS");
            List<Produto> produtos = produtoService.ListarProdutos();

            if (produtos.Count > 0)
            {
                Console.WriteLine($"{"ID",-5} | {"NOME",-20} | {"QUANTIDADE",10} | {"DISPONÍVEL?",10} | {"VALOR"}");
                Console.WriteLine(new string('-', 65));
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
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void ListarProdutosPorNome(string nome)
        {
            List<Produto> produtos = produtoService.ListarProdutosPorNome(nome);

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
            Console.WriteLine("Digite qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void EditarProduto()
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
                    ListarProdutosPorNome(nome);
                    break;

                case 2:
                    ListarProdutos();
                    break;
            }

            Console.Write("\nDigite o Id do produto: ");
            int id = int.Parse(Console.ReadLine());
            Produto produto = produtoService.BuscarProdutoPorId(id);
            if (produto != null)
            {
                
                produto = FormularioDeProduto();
                produto.Id = id;

                if (produtoService.AtualizarProduto(produto))
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

        public void DeletarProduto()
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
                    ListarProdutosPorNome(nome);
                    break;

                case 2:
                    ListarProdutos();
                    break;
            }

            Console.Write("\nDigite o Id do Produto: ");
            int id = int.Parse(Console.ReadLine());
            Produto produto = produtoService.BuscarProdutoPorId(id);
            if (produto != null)
            {
                if (produtoService.DeletarProduto(id)) Console.WriteLine("Produto deletado com sucesso!");
                else
                {
                    Console.WriteLine("Houve um erro na deleção do funcionário.");
                }
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Não há nenhum produto com o id especificado.");
                Console.ReadKey();
            }
        }
    }
}
