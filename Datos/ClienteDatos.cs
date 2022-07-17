using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public static class ClienteDatos
    {
        private static SqlConnection ConexionSql()
        {
            return new SqlConnection(Properties.Settings.Default.ConexionBd);
        }
        private static SqlCommand ComandoSql(SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public static ClienteEntidad Actualizar(ClienteEntidad clienteEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"UPDATE [dbo].[CLIENTES]
                                  SET [CED_CLI] = @CED_CLI
                                     ,[NOM_CLI] = @NOM_CLI
                                     ,[CEL_CLI] = @CEL_CLI
                                     ,[EMAIL] = @EMAIL
                                WHERE [ID_CLI]= @ID_CLI;";
                cmd.Parameters.AddWithValue("@ID_CLI", clienteEntidad.Id);
                cmd.Parameters.AddWithValue("@CED_CLI", clienteEntidad.Cedula);
                cmd.Parameters.AddWithValue("@NOM_CLI", clienteEntidad.Nombre);
                cmd.Parameters.AddWithValue("@CEL_CLI", clienteEntidad.Celular);
                cmd.Parameters.AddWithValue("@EMAIL", clienteEntidad.Email);

                cmd.ExecuteNonQuery();
                return clienteEntidad;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static ClienteEntidad DevolverClienteCedula(string text)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT [ID_CLI]
                                  ,[CED_CLI]
                                  ,[NOM_CLI]
                                  ,[CEL_CLI]
                                  ,[EMAIL]
                              FROM [CLIENTES]
                                  WHERE [CED_CLI] = @CED_CLI;";
                cmd.Parameters.AddWithValue("@CED_CLI", text);

                ClienteEntidad clienteEntidad = new ClienteEntidad();
                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        clienteEntidad.Id = Convert.ToInt32(dr["ID_CLI"].ToString());
                        clienteEntidad.Cedula = dr["CED_CLI"].ToString();
                        clienteEntidad.Nombre = dr["NOM_CLI"].ToString();
                        clienteEntidad.Celular = dr["CEL_CLI"].ToString();
                        clienteEntidad.Email = dr["EMAIL"].ToString();
                    }
                }
                return clienteEntidad;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static ClienteEntidad Guardar(ClienteEntidad clienteEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"INSERT INTO [dbo].[CLIENTES]
                                 ([CED_CLI]
                                 ,[NOM_CLI]
                                 ,[CEL_CLI]
                                 ,[EMAIL])
                           VALUES (@CED_CLI, @NOM_CLI, @CEL_CLI, @EMAIL);
                                 SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@CED_CLI", clienteEntidad.Cedula);
                cmd.Parameters.AddWithValue("@NOM_CLI", clienteEntidad.Nombre);
                cmd.Parameters.AddWithValue("@CEL_CLI", clienteEntidad.Celular);
                cmd.Parameters.AddWithValue("@EMAIL", clienteEntidad.Email);

                clienteEntidad.Id = Convert.ToInt32(cmd.ExecuteScalar());
                return clienteEntidad;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
