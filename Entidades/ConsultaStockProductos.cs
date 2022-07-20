using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ConsultaStockProductos
    {
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public ConsultaStockProductos()
        {

        }

        public ConsultaStockProductos(string nombre, int stock)
        {
            Nombre = nombre;
            Stock = stock;
        }
    }
}
