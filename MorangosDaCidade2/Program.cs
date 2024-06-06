using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MorangosDaCidade.Controllers;
using MorangosDaCidade2.Controllers;

namespace BancoMorangosDaCidade
{
    internal class Program
    {

        static void Main(string[] args)
        {
            ExibirOpcoesDoMenu();
        }

        public static void Login()
        {
            Console.WriteLine("     SEJA BEM-VINDO AO MORANGOS DA CIDADE!");
            Console.Write("Digite seu e-mail: ");
            String email = Console.ReadLine();
            Console.Write("Insira sua senha: ");
            String senha = Console.ReadLine();
        }

        public static void ExibirOpcoesDoMenu()
        {
            Dictionary<int, Controller> opcoes = new Dictionary<int, Controller>();
            opcoes.Add(1, new FuncionarioController());
            opcoes.Add(3, new ClienteController());
            opcoes.Add(-1, new SairController());

            int opcao = 0;
            while (opcao != -1)
            {
                Console.WriteLine("\nBem vindo! Escolha o menu que deseja acessar: ");
                Console.WriteLine("1 - Funcionários");
                Console.WriteLine("2 - Pedidos");
                Console.WriteLine("3 - Clientes");
                Console.WriteLine("4 - Produtos");
                Console.WriteLine("-1 - Sair");
                Console.Write("Escolha uma opção: ");
                string opDigitada = Console.ReadLine();
                opcao = int.Parse(opDigitada);
                if (opcoes.ContainsKey(opcao))
                {
                    Controller menu = opcoes[opcao];
                    menu.Executar();
                    if (opcao > 0) ExibirOpcoesDoMenu();
                }
            }
            
        }
    }
}

