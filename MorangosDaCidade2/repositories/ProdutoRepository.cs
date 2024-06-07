using MorangosDaCidade.Entities;
using MorangosDaCidade2.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace MorangosDaCidade2.repositories
{
    class ProdutoRepository
    {
        string stringDeConexao = @"Data Source=LAPTOP-V1LI7TEI;Initial Catalog=MorangosDaCidade;Integrated Security=True";

        public async Task<int> CadastrarProdutoAsync(Produto p)
        {
            int resultado = 0;

            string query = "INSERT INTO dbo.PRODUTO (Nome, Descricao, Quantidade," +
                "Disponivel, Valor)" +
                " VALUES (@Nome, @Descricao, @Quantidade, @Disponivel, @Valor)";

            using (SqlConnection conexao = new SqlConnection(stringDeConexao))
            {
                SqlCommand comando = new SqlCommand(query, conexao);
                comando.Parameters.AddWithValue("@Nome", p.Nome);
                comando.Parameters.AddWithValue("@Descricao", p.Descricao);
                comando.Parameters.AddWithValue("@Quantidade", p.Quantidade);
                comando.Parameters.AddWithValue("@Disponivel", p.Disponivel);
                comando.Parameters.AddWithValue("@Valor", p.Valor);

                try
                {
                    await conexao.OpenAsync();

                    resultado = (int)await comando.ExecuteNonQueryAsync();
                    Console.WriteLine("Número de linhas afetadas: " + resultado);
                }
                catch (SqlException ex) { Console.WriteLine("Erro de SQL: " + ex.Message); }

                catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }

                finally
                {
                    // Garantindo que a conexão seja fechada
                    conexao.Close();
                }
            }
            return resultado;
        }

        public async Task<List<Produto>> ListarFuncionariosAsync()
        {
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT IdProduto, Nome, Descricao, Quantidade, Disponivel, Valor FROM dbo.PRODUTO";
                    SqlCommand comando = new SqlCommand(query, connection);
                    List<Produto> produtos = new List<Produto>();
                    SqlDataReader reader = await comando.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Produto p = new Produto();
                        p.Id = (int)reader["IdProduto"];
                        p.Nome = (string)reader["Nome"];
                        p.Descricao = (string)reader["Descricao"];
                        p.Quantidade = (int)reader["Quantidade"];
                        p.Disponivel = (bool)reader["Disponivel"];
                        p.Valor = (double)reader["Valor"];
                        
                        produtos.Add(p);
                    }
                    return produtos;
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

        public async Task<List<Produto>> BuscarProdutoPorNomeAsync(string nome)
        {
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT IdProduto, Nome, Descricao, Quantidade, Disponivel, Valor FROM dbo.PRODUTO" +
                        " WHERE NOME LIKE @Nome";
                    SqlCommand comando = new SqlCommand(query, connection);
                    comando.Parameters.AddWithValue("@Nome", $"%{nome}%");
                    List<Produto> produtos = new List<Produto>();
                    SqlDataReader reader = await comando.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Produto p = new Produto();
                        p.Id = (int)reader["IdProduto"];
                        p.Nome = (string)reader["Nome"];
                        p.Descricao = (string)reader["Descricao"];
                        p.Quantidade = (int)reader["Quantidade"];
                        p.Disponivel = (bool)reader["Disponivel"];
                        p.Valor = (double)reader["Valor"];
                        produtos.Add(p);
                    }
                    return produtos;
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

        public async Task<Produto> BuscarProdutoPorIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT IdProduto, Nome, Descricao, Quantidade, Disponivel, Valor FROM dbo.PRODUTO" +
                        " WHERE IdProduto = @Id";
                    SqlCommand comando = new SqlCommand(query, connection);
                    comando.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = await comando.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        Produto p = new Produto();
                        p.Id = (int)reader["IdProduto"];
                        p.Nome = (string)reader["Nome"];
                        p.Descricao = (string)reader["Descricao"];
                        p.Quantidade = (int)reader["Quantidade"];
                        p.Disponivel = (bool)reader["Disponivel"];
                        p.Valor = (double)reader["Valor"];
                        connection.Close();
                        return p;
                    }

                }
                catch (SqlException ex) { Console.WriteLine("Erro de SQL: " + ex.Message); }
                catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
                connection.Close();
            }
            return null;
        }

        public async Task<int> AtualizarProdutoAsync(Produto produto)
        {
            int resultado = 0;
            string query = "UPDATE dbo.PRODUTO SET Nome = @NovoNome, Descricao = @NovaDescricao, " +
                    "Quantidade = @NovaQuantidade, Disponivel = @NovoDisponivel, Valor = @NovoValor WHERE IdProduto = @Id";

            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", produto.Id);
                command.Parameters.AddWithValue("@NovoNome", produto.Nome);
                command.Parameters.AddWithValue("@NovaDescricao", produto.Descricao);
                command.Parameters.AddWithValue("@NovaQuantidade", produto.Quantidade);
                command.Parameters.AddWithValue("@NovoDisponivel", produto.Disponivel);
                command.Parameters.AddWithValue("@NovoValor", produto.Valor);

                try
                {
                    await connection.OpenAsync();
                    resultado =(int)await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Número de linhas afetadas: " + resultado);
                }
                catch (SqlException ex) { Console.WriteLine("Erro de SQL: " + ex.Message); }

                catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }

                finally
                {
                    connection.Close();
                }
            }
            return resultado;
        }

        public async Task<int> DeletarProdutoAsync(int id)
        {
            int resultado = 0;
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "DELETE FROM dbo.PRODUTO WHERE IdProduto = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    resultado = await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Número de linhas afetadas: " + resultado);
                }
                catch (SqlException ex) { Console.WriteLine($"Erro de SQL: {ex.Message}"); }
                catch (Exception ex) { Console.WriteLine($"Erro: {ex.Message}"); }
            }
            return resultado;
        }
    }
}
