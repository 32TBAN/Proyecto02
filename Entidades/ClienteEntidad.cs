using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ClienteEntidad
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public ClienteEntidad()
        {

        }

        public ClienteEntidad(int id, string cedula, string nombre, string celular, string email)
        {
            Id = id;
            Cedula = cedula;
            Nombre = nombre;
            Celular = celular;
            Email = email;
        }
    }
}
