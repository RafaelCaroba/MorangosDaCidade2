using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MorangosDaCidade.Repository
{

    class ClienteRepository
    {
        public void InserirCliente()
        {
            int IdUsuario = 2;
            string NOME = "João";
            string CPF = "99999999999";
            string TELEFONE = "8888888888";
            string EMAIL = "joão@gmail.com";
            DateTime DataNascimento = new DateTime(2000, 10, 10);
            string SENHA = "1234";

            //string query = "SELECT * FROM dbo.FazendaCadastrp";


            string stringDeConexao = @"Data Source=VALTENCIR\SQLEXPRESS;Initial Catalog=MorangosDaCidade;Integrated Security=True";

            string consulta = "INSERT INTO dbo.CLIENTE (IdUsuario, NOME,CPF,TELEFONE,EMAIL,DataNascimento,SENHA)" + " VALUES (@IdUsuario, @NOME, @CPF, @TELEFONE, @EMAIL, @DataNascimento, @SENHA)";
            //primeiro é o nome exato da tabela e o segundo apelidos somente.

            using (SqlConnection conexao = new SqlConnection(stringDeConexao))
            {
                SqlCommand comando = new SqlCommand(consulta, conexao);
                comando.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                comando.Parameters.AddWithValue("@NOME", NOME);
                comando.Parameters.AddWithValue("@CPF", CPF);
                comando.Parameters.AddWithValue("@TELEFONE", TELEFONE);
                comando.Parameters.AddWithValue("@EMAIL", EMAIL);
                comando.Parameters.AddWithValue("@DataNascimento", DataNascimento);
                comando.Parameters.AddWithValue("@SENHA", SENHA);
                //conexao.Open();

                try
                {
                    // Abrindo a conexão com o banco de dados
                    conexao.Open();

                    // Executando o comando SQL (INSERT)
                    int resultado = comando.ExecuteNonQuery();
                    Console.WriteLine("Número de linhas afetadas: " + resultado);
                }
                catch (SqlException ex)
                {
                    // Tratando erros de SQL
                    Console.WriteLine("Erro de SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Tratando outros erros
                    Console.WriteLine("Erro: " + ex.Message);
                }
                finally
                {
                    // Garantindo que a conexão seja fechada
                    conexao.Close();
                }
            }

            // Mantendo o console aberto para visualizar os resultados
            Console.ReadLine();

            /*int resultado = comando.ExecuteNonQuery();
                Console.WriteLine(resultado);
                Console.ReadLine();*/

        }

    }
}