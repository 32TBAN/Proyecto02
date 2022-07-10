using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public static class BodegaDatos
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

        public static BodegaEntidad BuscarBodega(int num)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT [NUM_BOD]
                                  ,[UBI_BOD]
                                  ,[CAPACIDAD]
                                  FROM [dbo].[BODEGAS]
                                  WHERE [NUM_BOD] = @NUM;";
                cmd.Parameters.AddWithValue("@NUM",num);

                BodegaEntidad bodegaEntidad = new BodegaEntidad();
                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    bodegaEntidad.Num = Convert.ToInt32(dr["NUM_BOD"].ToString());
                    bodegaEntidad.Ubicacion = dr["UBI_BOD"].ToString();
                    bodegaEntidad.Capacidad = Convert.ToInt32(dr["CAPACIDAD"].ToString());
                }

                return bodegaEntidad;
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

        public static bool EliminarBodega(int num)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"DELETE FROM [dbo].[BODEGAS]
                                    WHERE [NUM_BOD]=@NUM;";
                cmd.Parameters.AddWithValue("@NUM", num);

                var filasAfectadas = Convert.ToInt32(cmd.ExecuteNonQuery());

                return filasAfectadas > 0;
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

        public static List<BodegaEntidad> DevolverBodegas()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT [NUM_BOD]
                                  ,[UBI_BOD]
                                  ,[CAPACIDAD]
                                  FROM [dbo].[BODEGAS];";

                List<BodegaEntidad> listaBodegas = new List<BodegaEntidad>();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaBodegas.Add(new BodegaEntidad(
                            Convert.ToInt32(dr["NUM_BOD"].ToString()), dr["UBI_BOD"].ToString(),
                            Convert.ToInt32(dr["CAPACIDAD"].ToString())));
                    }
                }

                return listaBodegas;
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

        public static BodegaEntidad Guardar(BodegaEntidad bodegaEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"INSERT INTO [dbo].[BODEGAS]
                                 ([UBI_BOD]
                                 ,[CAPACIDAD])
                                 VALUES (@UBI_BOD, @CAPACIDAD);
                                 SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@UBI_BOD", bodegaEntidad.Ubicacion);
                cmd.Parameters.AddWithValue("@CAPACIDAD", bodegaEntidad.Capacidad);


                bodegaEntidad.Num = Convert.ToInt32(cmd.ExecuteScalar());
                return bodegaEntidad;
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
