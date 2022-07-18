using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public static class VentaDatos
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

        public static VentaEntidad Actualizar(VentaEntidad ventaEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"UPDATE [dbo].[VENTAS]
                                  SET [ID_USU_VEN] = @ID_USU_VEN
                                     ,[ID_CLI_VEN] = @ID_CLI_VEN
                                     ,[TOTAL] = @TOTAL
                                     ,[FEC_VENTA] = @FEC_VENTA
                                     ,[ESTADO] = @ESTADO
                                WHERE [NUM_VEN]=@NUM_VEN;";
                cmd.Parameters.AddWithValue("@NUM_VEN", ventaEntidad.Num);
                cmd.Parameters.AddWithValue("@ID_USU_VEN", ventaEntidad.IdUsuario);
                cmd.Parameters.AddWithValue("@ID_CLI_VEN", ventaEntidad.IdCliente);
                cmd.Parameters.AddWithValue("@TOTAL", ventaEntidad.Total);
                cmd.Parameters.AddWithValue("@FEC_VENTA", ventaEntidad.Fecha);
                cmd.Parameters.AddWithValue("@ESTADO", ventaEntidad.Estado);

                cmd.ExecuteNonQuery();
                return ventaEntidad;
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

        public static bool EliminarProductoDetalle(int codProducto, int numVenta)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"DELETE FROM [dbo].[DETALLE_VENTAS]
                                    WHERE [COD_PRO_VEN]=@NUM_PRO AND [NUM_VEN_PER]=@NUM_VENT;";
                cmd.Parameters.AddWithValue("@NUM_PRO", codProducto);
                cmd.Parameters.AddWithValue("@NUM_VENT", numVenta);

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

        public static List<DetalleVentaEntidad> DevolverDetalle(int num)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT DV.NUM_VEN_PER AS NUM_VEN, 
                                     DV.COD_PRO_VEN AS COD_PRO, 
                                     P.NOM_PRO AS NOM_PRO,
                                     DV.CANTIDAD AS CANTIDAD, 
                                     DV.SUBTOTAL AS SUBTOTAL, 
                                     DV.NUM_BOD_VEN AS NUM_BOD_PER
                                   FROM DETALLE_VENTAS DV
                                   INNER JOIN PRODUCTOS P ON P.COD_PRO = DV.COD_PRO_VEN
                                   WHERE DV.NUM_VEN_PER = @NUM_REC_VEN;";
                cmd.Parameters.AddWithValue("@NUM_REC_VEN", num);
                List<DetalleVentaEntidad> listaDetalleVenta = new List<DetalleVentaEntidad>();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                    {
                        return null;
                    }
                    while (dr.Read())
                    {
                        listaDetalleVenta.Add(new DetalleVentaEntidad(
                            Convert.ToInt32(dr["NUM_VEN"].ToString()),
                            Convert.ToInt32(dr["COD_PRO"].ToString()),
                            dr["NOM_PRO"].ToString(),
                            Convert.ToInt32(dr["CANTIDAD"].ToString()),
                            Convert.ToSingle(dr["SUBTOTAL"].ToString()),
                            Convert.ToInt32(dr["NUM_BOD_PER"].ToString())));
                    }
                }
                return listaDetalleVenta;
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

        public static DetalleVentaEntidad ActualizarDetalle(DetalleVentaEntidad detalleVentaEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"UPDATE [dbo].[DETALLE_VENTAS]
                               SET [CANTIDAD] = @CANTIDAD
                                  ,[SUBTOTAL] = @SUBTOTAL
                                  ,[NUM_BOD_VEN] = @NUM_BOD_VEN
                             WHERE [NUM_VEN_PER] = @NUM_VEN_PER AND [COD_PRO_VEN] = @COD_PRO_VEN;";

                cmd.Parameters.AddWithValue("@NUM_VEN_PER", detalleVentaEntidad.NumVenta);
                cmd.Parameters.AddWithValue("@COD_PRO_VEN", detalleVentaEntidad.CodProducto);

                cmd.Parameters.AddWithValue("@CANTIDAD", detalleVentaEntidad.Cantidad);
                cmd.Parameters.AddWithValue("@SUBTOTAL", detalleVentaEntidad.Subtotal);
                cmd.Parameters.AddWithValue("@NUM_BOD_VEN", detalleVentaEntidad.NumBodega);

                cmd.ExecuteNonQuery();
                return detalleVentaEntidad;
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

        public static DetalleVentaEntidad GuardarDetalle(DetalleVentaEntidad detalleVentaEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"INSERT INTO [dbo].[DETALLE_VENTAS]
                                    ([NUM_VEN_PER]
                                    ,[COD_PRO_VEN]
                                    ,[CANTIDAD]
                                    ,[SUBTOTAL]
                                    ,[NUM_BOD_VEN])
                              VALUES(@NUM_VEN_PER, @COD_PRO_VEN, @CANTIDAD, @SUBTOTAL, @NUM_BOD_VEN);

                             UPDATE [dbo].[BODEGAS]
                             SET [CAPACIDAD] = [CAPACIDAD]+@CANTIDAD
                             WHERE NUM_BOD = @NUM_BOD_VEN;

                             UPDATE [dbo].[PRODUCTOS]
                             SET [STOCK] = [STOCK]-@CANTIDAD
                             WHERE [COD_PRO] = @COD_PRO_VEN;

                             UPDATE [dbo].[CONTENIDO_BODEGA]
                             SET [CANTIDAD] = [CANTIDAD]-@CANTIDAD
                             WHERE [NUM_BOD_CON] = @NUM_BOD_VEN AND [COD_PRO_CON] = @COD_PRO_VEN;

                             SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@NUM_VEN_PER", detalleVentaEntidad.NumVenta);
                cmd.Parameters.AddWithValue("@COD_PRO_VEN", detalleVentaEntidad.CodProducto);
                cmd.Parameters.AddWithValue("@CANTIDAD", detalleVentaEntidad.Cantidad);
                cmd.Parameters.AddWithValue("@SUBTOTAL", detalleVentaEntidad.Subtotal);
                cmd.Parameters.AddWithValue("@NUM_BOD_VEN", detalleVentaEntidad.NumBodega);

                if (cmd.ExecuteNonQuery() != 0)
                {
                    return detalleVentaEntidad;
                }
                else
                {
                    return null;
                }
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

        public static VentaEntidad Guardar(VentaEntidad ventaEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"INSERT INTO [dbo].[VENTAS]
                                 ([ID_USU_VEN]
                                 ,[ID_CLI_VEN]
                                 ,[TOTAL]
                                 ,[FEC_VENTA]
                                 ,[ESTADO])
                           VALUES(@ID_USU_VEN ,@ID_CLI_VEN ,@TOTAL ,@FEC_VENTA, @ESTADO);
                                 SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@ID_USU_VEN", ventaEntidad.IdUsuario);
                cmd.Parameters.AddWithValue("@ID_CLI_VEN", ventaEntidad.IdCliente);
                cmd.Parameters.AddWithValue("@TOTAL", ventaEntidad.Total);
                cmd.Parameters.AddWithValue("@FEC_VENTA", ventaEntidad.Fecha);
                cmd.Parameters.AddWithValue("@ESTADO", ventaEntidad.Estado);

                ventaEntidad.Num = Convert.ToInt32(cmd.ExecuteScalar());
                return ventaEntidad;
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
