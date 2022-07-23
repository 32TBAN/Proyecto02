using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

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

        public static List<ListaConsulta1> ConsultaMontoFecha(ConsultasEntidad consultaDatos)
        {
            try
            {
                List<ListaConsulta1> listaConsultaR = new List<ListaConsulta1>();
                var listaConsulta = new List<KeyValuePair<DateTime, float>>();
                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT FEC_VENTA, SUM(TOTAL) MONTO
                                            FROM VENTAS
                                            WHERE FEC_VENTA BETWEEN @fechaInicio AND @fechaFin
                                            GROUP BY FEC_VENTA;";
                        cmd.Parameters.AddWithValue("@fechaInicio", consultaDatos.FechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", consultaDatos.FechaFin);

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read()) 
                            {
                                if (dr.HasRows)
                                {
                                    listaConsulta.Add(new KeyValuePair<DateTime, float>(
                                        Convert.ToDateTime(dr[0]),Convert.ToSingle(dr[1])));
                                }
                            }
                        }

                      if (consultaDatos.NumDias <= 30)
                        {
                            listaConsultaR = (from orderList in listaConsulta
                                              group orderList by orderList.Key.ToString("dd MMM")
                                                 into order
                                                select new ListaConsulta1
                                                {
                                                    Fecha = order.Key,
                                                    Monto = order.Sum(amount => amount.Value)
                                                }).ToList();
                        }//Semanas
                        else if (consultaDatos.NumDias <= 92)
                        {
                            listaConsultaR = (from orderList in listaConsulta
                                              group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                                    orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                               into order
                                                select new ListaConsulta1
                                                {
                                                    Fecha = "Week " + order.Key.ToString(),
                                                    Monto = order.Sum(amount => amount.Value)
                                                }).ToList();
                        }
                        //Meses
                        else if (consultaDatos.NumDias <= (365 * 2))
                        {
                            bool isYear = consultaDatos.NumDias <= 365 ? true : false;
                            listaConsultaR = (from orderList in listaConsulta
                                              group orderList by orderList.Key.ToString("MMM yyyy")
                                               into order
                                                select new ListaConsulta1
                                                {
                                                    Fecha = isYear ? order.Key.Substring(0, order.Key.IndexOf(" ")) : order.Key,
                                                    Monto = order.Sum(amount => amount.Value)
                                                }).ToList();
                        }
                        //Anios
                        else
                        {
                            listaConsultaR = (from orderList in listaConsulta
                                              group orderList by orderList.Key.ToString("yyyy")
                                               into order
                                                select new ListaConsulta1
                                                {
                                                    Fecha = order.Key,
                                                    Monto = order.Sum(amount => amount.Value)
                                                }).ToList();
                        }
                    }
                }
                return listaConsultaR;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ConsultaTopProductos> ConsultaTopProductos(ConsultasEntidad consultaDatos)
        {
            try
            {
                List<ConsultaTopProductos> consultas = new List<ConsultaTopProductos>();
                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT TOP (5) P.NOM_PRO AS NOMBRE, SUM(DV.CANTIDAD) AS CANTIDAD
                                            FROM DETALLE_VENTAS DV
                                            INNER JOIN PRODUCTOS P ON P.COD_PRO = DV.COD_PRO_VEN
                                            INNER JOIN VENTAS V ON V.NUM_VEN = DV.NUM_VEN_PER
                                            WHERE V.FEC_VENTA BETWEEN @fechaInicio AND @fechaFin
                                            GROUP BY P.NOM_PRO
                                            ORDER BY CANTIDAD DESC;";
                        cmd.Parameters.AddWithValue("@fechaInicio", consultaDatos.FechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", consultaDatos.FechaFin);

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (dr.HasRows)
                                {
                                    consultas.Add(new ConsultaTopProductos(dr["NOMBRE"].ToString(),
                                        Convert.ToInt32(dr["CANTIDAD"].ToString())));
                                }
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
        }

        public static List<ConsultaStockProductos> ConsultaStockFecha(ConsultasEntidad consultaDatos)
        {
            try
            {
                List<ConsultaStockProductos> consultas = new List<ConsultaStockProductos>();
                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT P.NOM_PRO AS NOMBRE, SUM(CB.CANTIDAD) AS CANTIDAD
                                             FROM CONTENIDO_BODEGA CB
                                             INNER JOIN PRODUCTOS P ON CB.COD_PRO_CON = P.COD_PRO
                                             GROUP BY P.NOM_PRO;";
                        cmd.Parameters.AddWithValue("@fechaInicio", consultaDatos.FechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", consultaDatos.FechaFin);

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (dr.HasRows)
                                {
                                    consultas.Add(new ConsultaStockProductos(dr["NOMBRE"].ToString(),
                                        Convert.ToInt32(dr["CANTIDAD"].ToString())));
                                }
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
        }

        public static ConsultasEntidad DevolverConsulta(ConsultasEntidad consultaDatos)
        {
            try
            {
              
                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT COUNT(NUM_VEN)
                                            FROM VENTAS
                                            WHERE FEC_VENTA BETWEEN @fechaInicio AND @fechaFin;";
                        cmd.Parameters.AddWithValue("@fechaInicio", consultaDatos.FechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", consultaDatos.FechaFin);

                        consultaDatos.NumVentas = (int)cmd.ExecuteScalar();
                        connection.Close();
                    }
                }

                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT COUNT(DISTINCT ID_CLI_VEN)
                                            FROM VENTAS 
                                            WHERE FEC_VENTA BETWEEN @fechaInicio AND @fechaFin;";
                        cmd.Parameters.AddWithValue("@fechaInicio", consultaDatos.FechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", consultaDatos.FechaFin);

                        consultaDatos.NumClientes = (int)cmd.ExecuteScalar();
                        connection.Close();

                    }
                }

                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT COUNT(DISTINCT ID_PRO_REC)
                                            FROM RECEPCION_PRO
                                            WHERE FEC_REC BETWEEN @fechaInicio AND @fechaFin;";
                        cmd.Parameters.AddWithValue("@fechaInicio", consultaDatos.FechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", consultaDatos.FechaFin);

                        consultaDatos.NumProductores = (int)cmd.ExecuteScalar();
                        connection.Close();

                    }
                }

                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT COUNT(DISTINCT dv.COD_PRO_VEN) AS NUMPRODUCTOS
                                            FROM DETALLE_VENTAS dv
                                            INNER JOIN VENTAS V ON V.NUM_VEN = dv.NUM_VEN_PER
                                            WHERE V.FEC_VENTA BETWEEN @fechaInicio AND @fechaFin;";
                        cmd.Parameters.AddWithValue("@fechaInicio", consultaDatos.FechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", consultaDatos.FechaFin);

                        consultaDatos.NumProductos = (int)cmd.ExecuteScalar();
                        connection.Close();

                    }
                }

                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT CASE WHEN SUM(TOTAL) IS NULL THEN 0
                                            ELSE SUM(TOTAL) END AS TOTAL
                                            FROM VENTAS 
                                            WHERE FEC_VENTA BETWEEN @fechaInicio AND @fechaFin;";
                        cmd.Parameters.AddWithValue("@fechaInicio", consultaDatos.FechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", consultaDatos.FechaFin);


                        consultaDatos.TotalIngresos = Convert.ToSingle(cmd.ExecuteScalar());
            
                        connection.Close();

                    }
                }

                using (var connection = ConexionSql())
                {
                    using (var cmd = ComandoSql(connection))
                    {
                        connection.Open();
                        cmd.CommandText = @"SELECT CASE WHEN SUM(TOTAL) IS NULL THEN 0
                                            ELSE SUM(TOTAL) END AS TOTAL
                                            FROM RECEPCION_PRO
                                            WHERE FEC_REC BETWEEN @fechaInicio AND @fechaFin;";
                        cmd.Parameters.AddWithValue("@fechaInicio", consultaDatos.FechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", consultaDatos.FechaFin);
      
                        consultaDatos.TotalBeneficio = Convert.ToSingle(cmd.ExecuteScalar());
                        
                        connection.Close();

                    }
                }
                consultaDatos.TotalBeneficio -= consultaDatos.TotalIngresos;
                if (consultaDatos.TotalBeneficio < 1)
                {
                    consultaDatos.TotalBeneficio = 0;
                }
                return consultaDatos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
