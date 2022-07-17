using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleVentaEntidad
    {
        public int NumVenta { get; set; }
        public int CodProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public float Subtotal { get; set; }
        public int NumBodega { get; set; }
        public DetalleVentaEntidad()
        {

        }

        public DetalleVentaEntidad(int numVenta, int codProducto, string nombreProducto, int cantidad, float subtotal, int numBodega)
        {
            NumVenta = numVenta;
            CodProducto = codProducto;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            Subtotal = subtotal;
            NumBodega = numBodega;
        }
    }
}
