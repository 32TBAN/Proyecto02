using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ProductosEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public ProductosEntidad()
        {

        }

        public ProductosEntidad(int id, string nombre, float precio)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
        }
    }
}
