using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ProductoresEntidad
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string TelefonoAdicional { get; set; }
        public string Email { get; set; }
        public ProductoresEntidad()
        {

        }
        public ProductoresEntidad(int id, string cedula, string nombre, string direccion, string telefono, string telefonoAdicional, string email)
        {
            Id = id;
            Cedula = cedula;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            TelefonoAdicional = telefonoAdicional;
            Email = email;
        }

    }
}
