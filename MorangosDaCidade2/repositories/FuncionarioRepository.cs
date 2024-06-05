using MorangosDaCidade.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorangosDaCidade.Repository
{
    class FuncionarioRepository
    {
        string stringDeConexao = @"Data Source=LAPTOP-V1LI7TEI;Initial Catalog=MorangosDaCidade;Integrated Security=True";
        public int CadastrarFuncionario(Funcionario f)
        {
            int resultado = 0;

            string consulta = "INSERT INTO dbo.FUNCIONARIO (NOME,CPF,TELEFONE," +
                "EMAIL,DataNascimento,SENHA)" + 
                " VALUES (@NOME, @CPF, @TELEFONE, @EMAIL, @DataNascimento, @SENHA)";

            using (SqlConnection conexao = new SqlConnection(stringDeConexao))
            {
                SqlCommand comando = new SqlCommand(consulta, conexao);
                comando.Parameters.AddWithValue("@NOME", f.Nome);
                comando.Parameters.AddWithValue("@CPF", f.Cpf);
                comando.Parameters.AddWithValue("@TELEFONE", f.Telefone);
                comando.Parameters.AddWithValue("@EMAIL", f.Email);
                comando.Parameters.AddWithValue("@DataNascimento", f.DataNascimento);
                comando.Parameters.AddWithValue("@SENHA", f.Senha);

                try
                {
                    conexao.Open();

                    resultado = comando.ExecuteNonQuery();
                    Console.WriteLine("Número de linhas afetadas: " + resultado);
                }
                catch (SqlException ex) { Console.WriteLine("Erro de SQL: " + ex.Message); }

                catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message);  }

                finally
                {
                    // Garantindo que a conexão seja fechada
                    conexao.Close();
                }
            }
            return resultado;
        }

        public List<Funcionario> ListarFuncionarios()
        {
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IdFunc, NOME, CPF, EMAIL, TELEFONE, DataNascimento FROM dbo.FUNCIONARIO";
                    SqlCommand comando = new SqlCommand(query, connection);
                    List<Funcionario> funcionarios = new List<Funcionario>();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        string nome = (string)reader["NOME"];
                        string cpf = (string)reader["CPF"];
                        string email = (string)reader["EMAIL"];
                        string telefone = (string)reader["TELEFONE"];
                        int id = (int)reader["IdFunc"];
                        Console.WriteLine(id);
                        SqlDateTime dtNascimento = reader.GetDateTime(reader.GetOrdinal("DataNascimento"));
                        Funcionario funcionario = new Funcionario(id, nome, cpf, email, telefone, dtNascimento, null);
                        funcionarios.Add(funcionario);
                    }
                    return funcionarios;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Erro de SQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return null;
        }

        public DataTable BuscarFuncionarioPorNome(string nome)
        {
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                connection.Open();
                string query = "SELECT * FROM Funcionarios WHERE Nome LIKE @Nome";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nome", "%" + nome + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public void AtualizarFuncionario(int id, string novoNome, string novoCpf, string novoEmail, 
            string novaSenha)
        {
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                connection.Open();
                string query = "UPDATE Funcionarios SET Nome = @NovoNome, CPF = @NovoCPF, " +
                    "Email = @NovoEmail, Senha = @NovaSenha WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@NovoNome", novoNome);
                command.Parameters.AddWithValue("@NovoCPF", novoCpf);
                command.Parameters.AddWithValue("@NovoEmail", novoEmail);
                command.Parameters.AddWithValue("@NovaSenha", novaSenha);
                command.ExecuteNonQuery();
            }
        }

        public void DeletarFuncionario(int id)
        {
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                connection.Open();
                string query = "DELETE FROM Funcionarios WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

    }
}