using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class UsuarioEntidad
    {
        public int IdUsuario { get; set; }
        public string Cedula { get; set; }
        public string Nickname { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public byte[] FotoPerfil { get; set; }
        public UsuarioEntidad()
        {

        }

        public UsuarioEntidad(int idUsuario, string cedula, string nickname, string nombre, string email, string tipo, byte[] fotoPerfil)
        {
            IdUsuario = idUsuario;
            Cedula = cedula;
            Nickname = nickname;
            Nombre = nombre;
            Email = email;
            Tipo = tipo;
            FotoPerfil = fotoPerfil;
        }
    }
}
