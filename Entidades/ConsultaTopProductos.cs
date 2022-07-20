using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ConsultaTopProductos
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public ConsultaTopProductos()
        {

        }

        public ConsultaTopProductos(string nombre, int cantidad)
        {
            Nombre = nombre;
            Cantidad = cantidad;
        }
    }
}
