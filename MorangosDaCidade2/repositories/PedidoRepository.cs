using MorangosDaCidade2.Entities;
using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MorangosDaCidade2.repositories
{
    internal class PedidoRepository
    {
        string stringDeConexao = @"Data Source=LAPTOP-V1LI7TEI;Initial Catalog=MorangosDaCidade;Integrated Security=True";

        public async Task<int> SalvarPedidoAsync(Pedido pedido)
        {
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO dbo.PEDIDO (IdCliente, DataPedido, StatusPedido) OUTPUT INSERTED.IdPedido" +
                            " VALUES (@ClienteId, GETDATE(), @StatusPedido)";

                        command.Parameters.AddWithValue("@ClienteId", pedido.Cliente.Id);
                        command.Parameters.AddWithValue("@StatusPedido", pedido.StatusPedido);

                        int idPedido = (int)await command.ExecuteScalarAsync();
                        return idPedido;
                    }
                    catch (SqlException ex){ Console.WriteLine(ex.Message); }
                    catch (InvalidCastException ex) { Console.WriteLine(ex.Message); }
                    catch (InvalidOperationException ex) { Console.WriteLine(ex.Message); }
                    catch (System.IO.IOException ex) { Console.WriteLine(ex.Message); }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
                return 0;
            }
        }

        public async Task<int> SalvarItemPedidoAsync(ItemPedido item, int idPedido)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            { 

                using (SqlCommand command = new SqlCommand())
                {
                    await connection.OpenAsync();
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO dbo.PEDIDO_PRODUTO (IdPedido, IdProduto, Quantidade)" +
                            " VALUES (@IdPedido, @IdProduto, @Quantidade)";

                        command.Parameters.AddWithValue("@IdProduto", item.Produto.Id);
                        command.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                        command.Parameters.AddWithValue("@IdPedido", idPedido);

                        
                        result = (int)await command.ExecuteNonQueryAsync();
                        //if (result > 0)
                        //{
                        //    Console.WriteLine("Passou aqui! Id: " + result);
                        //}
                        //else
                        //{
                        //    Console.WriteLine(result);
                        //}
                    }
                    catch(SqlException ex) { Console.WriteLine("Erro de SQL: " + ex.Message); }
                    catch (InvalidCastException ex) { Console.WriteLine(ex.Message); }
                    catch (InvalidOperationException ex) { Console.WriteLine(ex.Message); }
                    catch (System.IO.IOException ex) { Console.WriteLine(ex.Message); }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }

            }
            return result;
        }
    }
}
