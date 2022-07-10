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
    public static class ProductoresDatos
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
        public static ProductoresEntidad Guardar(ProductoresEntidad productoresEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"ps_InsertarProductores 
                                  @CED_PRO, 
                                  @NOM_PRO,
                                  @TEL_PRO,
                                  @TEL_PRO_ADI,
                                  @EMAIL,
                                  @DIR_PRO";
                cmd.Parameters.AddWithValue("@CED_PRO", productoresEntidad.Cedula);
                cmd.Parameters.AddWithValue("@NOM_PRO", productoresEntidad.Nombre);
                cmd.Parameters.AddWithValue("@TEL_PRO", productoresEntidad.Telefono);
                cmd.Parameters.AddWithValue("@TEL_PRO_ADI", productoresEntidad.TelefonoAdicional);
                cmd.Parameters.AddWithValue("@EMAIL", productoresEntidad.Email);
                cmd.Parameters.AddWithValue("@DIR_PRO", productoresEntidad.Direccion);

                productoresEntidad.Id = Convert.ToInt32(cmd.ExecuteScalar());
                return productoresEntidad;
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

        public static ProductoresEntidad Actualizar(ProductoresEntidad productoresEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"ps_ActualizarProductor
                                  @ID_PRO,
                                  @CED_PRO, 
                                  @NOM_PRO,
                                  @TEL_PRO,
                                  @TEL_PRO_ADI,
                                  @EMAIL,
                                  @DIR_PRO";
                cmd.Parameters.AddWithValue("@ID_PRO", productoresEntidad.Id);
                cmd.Parameters.AddWithValue("@CED_PRO", productoresEntidad.Cedula);
                cmd.Parameters.AddWithValue("@NOM_PRO", productoresEntidad.Nombre);
                cmd.Parameters.AddWithValue("@TEL_PRO", productoresEntidad.Telefono);
                cmd.Parameters.AddWithValue("@TEL_PRO_ADI", productoresEntidad.TelefonoAdicional);
                cmd.Parameters.AddWithValue("@EMAIL", productoresEntidad.Email);
                cmd.Parameters.AddWithValue("@DIR_PRO", productoresEntidad.Direccion);

                cmd.ExecuteNonQuery();
                return productoresEntidad;
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

        public static ProductoresEntidad DevolverProductorCedula(string cedula)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"sp_ProductorId @CED_PRO";
                cmd.Parameters.AddWithValue("@CED_PRO",cedula);

                ProductoresEntidad productoresEntidad = new ProductoresEntidad();
                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        productoresEntidad.Id = Convert.ToInt32(dr["ID_PRO"].ToString());
                        productoresEntidad.Cedula = dr["CED_PRO"].ToString();
                        productoresEntidad.Nombre = dr["NOM_PRO"].ToString();
                        productoresEntidad.Direccion = dr["DIR_PRO"].ToString();
                        productoresEntidad.Telefono = dr["TEL_PRO"].ToString();
                        productoresEntidad.TelefonoAdicional = dr["TEL_PRO_ADI"].ToString();
                        productoresEntidad.Email = dr["EMAIL"].ToString();
                    }
                }

                return productoresEntidad;
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
