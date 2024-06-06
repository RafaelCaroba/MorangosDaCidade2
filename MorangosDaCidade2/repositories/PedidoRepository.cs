using MorangosDaCidade2.Entities;
using System;
using System.Data.SqlClient;

namespace MorangosDaCidade2.repositories
{
    internal class PedidoRepository
    {
        string stringDeConexao = @"Data Source=LAPTOP-V1LI7TEI;Initial Catalog=MorangosDaCidade;Integrated Security=True";

        public int SalvarPedido(Pedido pedido)
        {
            using (SqlConnection connection = new SqlConnection(stringDeConexao))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO Pedido (Usuario, DataPedido, StatusPedido) OUTPUT INSERTED.Id VALUES" +
                            " (@Usuario, @DataPedido, @StatusPedido)";

                        command.Parameters.AddWithValue("@Usuario", pedido.Usuario);
                        command.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                        command.Parameters.AddWithValue("@StatusPedido", pedido.StatusPedido);

                        int idPedido = (int)command.ExecuteScalar();
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
    }
}
