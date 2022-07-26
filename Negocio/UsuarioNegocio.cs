using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
    public static class UsuarioNegocio
    {
        public static UsuarioEntidad BuscarUsuario(string text)
        {
            return UsuarioDatos.BuscarUsuario(text);
        }

        public static List<UsuarioEntidad> ListaUsuarios()
        {
            return UsuarioDatos.ListaUsuarios();
        }

        public static UsuarioEntidad Guardar(UsuarioEntidad usuarioEntidad)
        {
            if (usuarioEntidad.IdUsuario == 0)
            {
                return UsuarioDatos.Guardar(usuarioEntidad);
            }
            else
            {
                return UsuarioDatos.Actualizar(usuarioEntidad);
            }
        }

        public static string RecuperarContraseña(string text)
        {
            return UsuarioDatos.RecuperarContraseña(text);
        }

        public static UsuarioEntidad BuscarUsuarioNickname(string text)
        {
            return UsuarioDatos.BuscarUsuarioNickname(text);
        }

        public static bool EliminarUsuario(int idUsuario)
        {
            return UsuarioDatos.EliminarUsuario(idUsuario);
        }
    }
}
