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
