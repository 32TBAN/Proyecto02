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
    public static class RecepcionDatos
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

        public static bool AgregarProductoDetalle(List<DetalleRecepcionEntidad> detalleRecepcion)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                bool resultado = false;
                foreach (var item in detalleRecepcion)
                {
                    connection = ConexionSql();
                    SqlCommand cmd = ComandoSql(connection);
                    connection.Open();
                    cmd.CommandText = @"INSERT INTO [dbo].[DETALLE_RECEPCION]
                               ([NUM_REC_REC]
                               ,[COD_PRO_REC]
                               ,[CANTIDAD]
                               ,[SUBTOTAL]
                               ,[NUM_BOD_PER])
                         VALUES (@NUM_REC_REC, @COD_PRO_REC,@CANTIDAD,@SUBTOTAL,@NUM_BOD_PER);

                    UPDATE [dbo].[BODEGAS]
                    SET [CAPACIDAD] = [CAPACIDAD]-@CANTIDAD
                    WHERE NUM_BOD = @NUM_BOD_PER;

                    UPDATE [dbo].[PRODUCTOS]
                    SET [STOCK] = [STOCK]+@CANTIDAD
                    WHERE [COD_PRO] = @COD_PRO_REC;

                    UPDATE [dbo].[CONTENIDO_BODEGA]
                    SET [CANTIDAD] = [CANTIDAD]+@CANTIDAD
                    WHERE [NUM_BOD_CON] = @NUM_BOD_PER AND [COD_PRO_CON] = @COD_PRO_REC;
                    
                    SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.AddWithValue("@NUM_REC_REC", item.NumRecepcion);
                    cmd.Parameters.AddWithValue("@COD_PRO_REC", item.CodProducto);
                    cmd.Parameters.AddWithValue("@CANTIDAD", item.Cantidad);
                    cmd.Parameters.AddWithValue("@SUBTOTAL", item.Subtotal);
                    cmd.Parameters.AddWithValue("@NUM_BOD_PER", item.NumBodega);

                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                    connection.Close();
                }

                return resultado;
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

        public static List<DetalleRecepcionEntidad> DevolverProductosDetalle(int numRecepcion)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT DR.NUM_REC_REC AS NUM_REC, 
                                    DR.COD_PRO_REC AS COD_PRO, 
                                    P.NOM_PRO AS NOM_PRO,
                                    DR.CANTIDAD AS CANTIDAD, 
                                    DR.SUBTOTAL AS SUBTOTAL, 
                                    DR.NUM_BOD_PER AS NUM_BOD_PER
                                   FROM DETALLE_RECEPCION DR
                                   INNER JOIN PRODUCTOS P ON P.COD_PRO = DR.COD_PRO_REC
                                   WHERE DR.NUM_REC_REC = @NUM_REC_REC;";
                cmd.Parameters.AddWithValue("@NUM_REC_REC",numRecepcion);
                List<DetalleRecepcionEntidad> listaDetalleRecepcion = new List<DetalleRecepcionEntidad>();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaDetalleRecepcion.Add(new DetalleRecepcionEntidad(
                            Convert.ToInt32(dr["NUM_REC"].ToString()),
                            Convert.ToInt32(dr["COD_PRO"].ToString()),
                            dr["NOM_PRO"].ToString(),
                            Convert.ToInt32(dr["CANTIDAD"].ToString()),
                            Convert.ToSingle(dr["SUBTOTAL"].ToString()),
                            Convert.ToInt32(dr["NUM_BOD_PER"].ToString()) ));
                    }
                }
                return listaDetalleRecepcion;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public static RecepcionEntidad ComenzarRecepcion(RecepcionEntidad recepcionEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"INSERT INTO [dbo].[RECEPCION_PRO]
                                 ([ID_USU_REC]
                                 ,[ID_PRO_REC]
                                 ,[TOTAL]
                                 ,[FEC_REC])
                           VALUES(@ID_USU_REC, @ID_PRO_REC, @TOTAL, @FEC_REC);
                           SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@ID_USU_REC", recepcionEntidad.IdUsuario);
                cmd.Parameters.AddWithValue("@ID_PRO_REC", recepcionEntidad.IdProductor);
                cmd.Parameters.AddWithValue("@TOTAL", recepcionEntidad.Total);
                cmd.Parameters.AddWithValue("@FEC_REC", recepcionEntidad.FechaRecepcion);

                recepcionEntidad.NumRec = Convert.ToInt32(cmd.ExecuteScalar());
                return recepcionEntidad;
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
