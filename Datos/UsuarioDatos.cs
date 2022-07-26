using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace Datos
{
    public static class UsuarioDatos
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

        public static List<UsuarioEntidad> ListaUsuarios()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT [ID_USU]
                                 ,[CED_USU]
                                 ,[NICKNAME]
                                 ,[NOM_USU]
                                 ,[EMAIL]
                                 ,[TIPO]
                                 ,[IMG_USU]
                             FROM [Proyecto].[dbo].[USUARIOS];";

                List<UsuarioEntidad> listaUsuario = new List<UsuarioEntidad>();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaUsuario.Add(new UsuarioEntidad(Convert.ToInt32(dr["ID_USU"].ToString()),
                            dr["CED_USU"].ToString(), dr["NICKNAME"].ToString(), dr["NOM_USU"].ToString(), dr["EMAIL"].ToString(),
                            dr["TIPO"].ToString(), (byte[])dr["IMG_USU"]));
                    }
                }
                return listaUsuario;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static string RecuperarContraseña(string text)
        {
            try
            {
                UsuarioEntidad usuarioEntidad = BuscarUsuarioNickname(text);
  
                    if (usuarioEntidad == null)
                    {
                        return "No existe el nombre de usuario: " + text +
                            " Verifique e intente de nuevo";
                    }
                    string nombre = usuarioEntidad.Nombre;
                    string mail = usuarioEntidad.Email;
                    string contraseña = usuarioEntidad.Cedula;

                    var servicioMail = new MensajesEmail.SoporteEmail();
                    servicioMail.sendMail(
                        sujeto: "SYSTEM: Recuperacion de contraseña",
                        boby: "Hola " + nombre +
                        "\nTe recordamos que tu contraseña es:" + contraseña,
                        recipienteMail: new List<string> { mail });

                    return "Se ha enviado un mensaje a su correo";
            }
            catch (Exception)
            {
                return "Error no se ha encontrado su correo";
            }
        }

        public static bool EliminarUsuario(int idUsuario)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"DELETE FROM [dbo].[USUARIOS]
                                      WHERE [ID_USU]=@ID_USU";
                                     cmd.Parameters.AddWithValue("@ID_USU", idUsuario);
                var filasAfectadas = Convert.ToInt32(cmd.ExecuteNonQuery());

                return filasAfectadas > 0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static UsuarioEntidad BuscarUsuarioNickname(string text)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT [ID_USU]
                                  ,[CED_USU]
                                  ,[NICKNAME]
                                  ,[NOM_USU]
                                  ,[EMAIL]
                            	  ,[TIPO]
                                  ,[IMG_USU]
                              FROM [Proyecto].[dbo].[USUARIOS]
                            WHERE NICKNAME = @NICKNAME;";
                cmd.Parameters.AddWithValue("@NICKNAME", text);

                UsuarioEntidad usuarioEntidad = new UsuarioEntidad();
                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        usuarioEntidad.IdUsuario = Convert.ToInt32(dr["ID_USU"].ToString());
                        usuarioEntidad.Cedula = dr["CED_USU"].ToString();
                        usuarioEntidad.Nickname = dr["NICKNAME"].ToString();
                        usuarioEntidad.Nombre = dr["NOM_USU"].ToString();
                        usuarioEntidad.Email = dr["EMAIL"].ToString();
                        usuarioEntidad.Tipo = dr["TIPO"].ToString();
                        usuarioEntidad.FotoPerfil = (byte[])dr["IMG_USU"];
                    }
                    else
                    {
                        return null;
                    }
                }
                return usuarioEntidad;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static UsuarioEntidad Actualizar(UsuarioEntidad usuarioEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"UPDATE [dbo].[USUARIOS]
                               SET [CED_USU] = @CED_USU
                                  ,[NICKNAME] = @NICKNAME
                                  ,[NOM_USU] = @NOM_USU
                                  ,[EMAIL] = @EMAIL
                                  ,[TIPO] = @TIPO
                                  ,[IMG_USU] = @IMG_USU
                             WHERE ID_USU = @ID_USU;";

                cmd.Parameters.AddWithValue("@ID_USU", usuarioEntidad.IdUsuario);
                cmd.Parameters.AddWithValue("@CED_USU", usuarioEntidad.Cedula);
                cmd.Parameters.AddWithValue("@NICKNAME", usuarioEntidad.Nickname);
                cmd.Parameters.AddWithValue("@NOM_USU", usuarioEntidad.Nombre);
                cmd.Parameters.AddWithValue("@EMAIL", usuarioEntidad.Email);
                cmd.Parameters.AddWithValue("@TIPO", usuarioEntidad.Tipo);
                cmd.Parameters.AddWithValue("@IMG_USU", usuarioEntidad.FotoPerfil);


                cmd.ExecuteNonQuery();
                return usuarioEntidad;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static UsuarioEntidad Guardar(UsuarioEntidad usuarioEntidad)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"INSERT INTO [dbo].[USUARIOS]
                                  ([CED_USU]
                                  ,[NICKNAME]
                                  ,[NOM_USU]
                                  ,[EMAIL]
                                  ,[TIPO]
                                  ,[IMG_USU])
                            VALUES (@CED_USU,@NICKNAME,@NOM_USU,@EMAIL,@TIPO,@IMG_USU);
                                 SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@CED_USU", usuarioEntidad.Cedula);
                cmd.Parameters.AddWithValue("@NICKNAME", usuarioEntidad.Nickname);
                cmd.Parameters.AddWithValue("@NOM_USU", usuarioEntidad.Nombre);
                cmd.Parameters.AddWithValue("@EMAIL", usuarioEntidad.Email);
                cmd.Parameters.AddWithValue("@TIPO", usuarioEntidad.Tipo);
                cmd.Parameters.AddWithValue("@IMG_USU", usuarioEntidad.FotoPerfil);

                usuarioEntidad.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                return usuarioEntidad;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static UsuarioEntidad BuscarUsuario(string text)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = ConexionSql();
                SqlCommand cmd = ComandoSql(connection);
                connection.Open();
                cmd.CommandText = @"SELECT [ID_USU]
                                  ,[CED_USU]
                                  ,[NICKNAME]
                                  ,[NOM_USU]
                                  ,[EMAIL]
                            	  ,[TIPO]
                                  ,[IMG_USU]
                              FROM [Proyecto].[dbo].[USUARIOS]
                            WHERE [CED_USU] = @CED_USU;";
                cmd.Parameters.AddWithValue("@CED_USU", text);

                UsuarioEntidad usuarioEntidad = new UsuarioEntidad();
                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        usuarioEntidad.IdUsuario = Convert.ToInt32(dr["ID_USU"].ToString());
                        usuarioEntidad.Cedula = dr["CED_USU"].ToString();
                        usuarioEntidad.Nickname = dr["NICKNAME"].ToString();
                        usuarioEntidad.Nombre = dr["NOM_USU"].ToString();
                        usuarioEntidad.Email = dr["EMAIL"].ToString();
                        usuarioEntidad.Tipo = dr["TIPO"].ToString();
                        usuarioEntidad.FotoPerfil = (byte[])dr["IMG_USU"];
                    }
                    else
                    {
                        return null;
                    }
                }
                return usuarioEntidad;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }




    }
}
