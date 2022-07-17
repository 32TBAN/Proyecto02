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
    public static class ProductoDatos
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

        public static ProductosEntidad BuscarProducto(int id)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT [COD_PRO]
                                 ,[NOM_PRO]
                                 ,[PRE_PRO]
                                  FROM [dbo].[PRODUCTOS]
                                  WHERE [COD_PRO] = @COD;";
                cmd.Parameters.AddWithValue("@COD", id);

                ProductosEntidad productoEntidad = new ProductosEntidad();
                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    productoEntidad.Id = Convert.ToInt32(dr["COD_PRO"].ToString());
                    productoEntidad.Nombre = dr["NOM_PRO"].ToString();
                    productoEntidad.Precio = Convert.ToSingle(dr["PRE_PRO"].ToString());
                }

                return productoEntidad;
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

        public static bool EliminarProducto(int id)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"DELETE FROM [dbo].[PRODUCTOS]
                                    WHERE [COD_PRO]=@COD;";
                cmd.Parameters.AddWithValue("@COD", id);

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

        public static List<ProductosEntidad> DevolverProductos()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT [COD_PRO]
                                  ,[NOM_PRO]
                                  ,[PRE_PRO]
                                  ,[STOCK]
                                  FROM [dbo].[PRODUCTOS];";

                List<ProductosEntidad> listaProducto= new List<ProductosEntidad>();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaProducto.Add(new ProductosEntidad(
                            Convert.ToInt32(dr["COD_PRO"].ToString()), dr["NOM_PRO"].ToString(),
                            Convert.ToSingle(dr["PRE_PRO"].ToString()), Convert.ToInt32(dr["STOCK"].ToString())));
                    }
                }

                return listaProducto;
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

        public static ProductosEntidad Guardar(ProductosEntidad productosEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"INSERT INTO [dbo].[PRODUCTOS]
                                 ([NOM_PRO]
                                 ,[PRE_PRO])
                                 VALUES (@NOM_PRO, @PRE_PRO);
                                 SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@NOM_PRO", productosEntidad.Nombre);
                cmd.Parameters.AddWithValue("@PRE_PRO", productosEntidad.Precio);


                productosEntidad.Id = Convert.ToInt32(cmd.ExecuteScalar());
                return productosEntidad;
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
