using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public static class ConsultaDatos
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
        public static ConsultasEntidad DevolverConsulta(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                ConsultasEntidad consultas = new ConsultasEntidad();
                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT COUNT(NUM_VEN) AS CANTIDADR
                                            FROM VENTAS
                                            WHERE FEC_VENTA BETWEEN @fechaInicio AND @fechaFin;";
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                        using (var dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            if (dr.HasRows)
                            {
                                consultas.NumVentas = Convert.ToInt32(dr["CANTIDADR"].ToString());
                            }
                        }
                    }
                }

                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT  COUNT(DISTINCT ID_CLI_VEN) as NumClientes
                                            FROM VENTAS 
                                            WHERE FEC_VENTA BETWEEN @fechaInicio AND @fechaFin;";
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                        using (var dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            if (dr.HasRows)
                            {
                                consultas.NumClientes = Convert.ToInt32(dr["NumClientes"].ToString());
                            }
                        }
                    }
                }
               
                return consultas;
            }
            catch (Exception)
            {
                throw;
            }

//            consultasEntidad.NumClientes = 3;
//            consultasEntidad.NumProductores = 4;
//            consultasEntidad.NumProductos = 4;
//            consultasEntidad.NumVentas = 3;
//            consultasEntidad.TotalIngresos = 7;
//            consultasEntidad.TotalBeneficio = 6;
        }
    }
}
