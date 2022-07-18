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

        public static BodegaEntidad Actualizar(BodegaEntidad bodegaEntidad)
        {
                SqlConnection connection = new SqlConnection();
                try
                {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"UPDATE [dbo].[BODEGAS]
                                 SET [UBI_BOD] = @UBI_BOD,
                                 [CAPACIDAD] = @CAPACIDAD
                                 WHERE [NUM_BOD] = @NUM_BOD;";
                cmd.Parameters.AddWithValue("@NUM_BOD", bodegaEntidad.Num);
                cmd.Parameters.AddWithValue("@UBI_BOD", bodegaEntidad.Ubicacion);
                cmd.Parameters.AddWithValue("@CAPACIDAD", bodegaEntidad.Capacidad);
                cmd.ExecuteNonQuery();
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

        public static List<DetalleRecepcionEntidad> ListaCantidadProductoBodega(int valor)
        {
            try
            {
                List<DetalleRecepcionEntidad> listaProductosBodega = new List<DetalleRecepcionEntidad>();
                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"select NUM_BOD_CON, SUM(CANTIDAD) AS CANTIDAD
                                            from CONTENIDO_BODEGA
                                            WHERE COD_PRO_CON = @COD_PRO
                                            GROUP BY NUM_BOD_CON;";
                        cmd.Parameters.AddWithValue("@COD_PRO", valor);
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DetalleRecepcionEntidad detalleRecepcionEntidad = new DetalleRecepcionEntidad();
                                detalleRecepcionEntidad.NumBodega = Convert.ToInt32(dr["NUM_BOD_CON"].ToString());
                                detalleRecepcionEntidad.Cantidad = Convert.ToInt32(dr["CANTIDAD"].ToString());
                                listaProductosBodega.Add(detalleRecepcionEntidad);
                            }
                        }
                    }
                }
                return listaProductosBodega;
            }
            catch (Exception)
            {
                throw;
            }
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

        public static List<DetalleRecepcionEntidad> ListaContenidoBodega(int numBodega)
        {
            try
            {
                List<DetalleRecepcionEntidad> listaProductosBodega = new List<DetalleRecepcionEntidad>();
                using (var connection  = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT P.NOM_PRO AS NOMBRE
                                          ,CB.CANTIDAD AS CANTIDAD
                                          FROM CONTENIDO_BODEGA CB
                                          INNER JOIN PRODUCTOS P ON P.COD_PRO = CB.COD_PRO_CON
                                         WHERE CB.NUM_BOD_CON = @NUM_BOD;";
                        cmd.Parameters.AddWithValue("@NUM_BOD", numBodega);
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DetalleRecepcionEntidad detalleRecepcionEntidad = new DetalleRecepcionEntidad();
                                detalleRecepcionEntidad.NombreProducto = dr["NOMBRE"].ToString();
                                detalleRecepcionEntidad.Cantidad = Convert.ToInt32(dr["CANTIDAD"].ToString());
                                listaProductosBodega.Add(detalleRecepcionEntidad);
                            }
                        }
                    }
                }
                return listaProductosBodega;
            }
            catch (Exception)
            {
                throw;
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
