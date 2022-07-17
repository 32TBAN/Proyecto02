using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleRecepcionEntidad
    {
        public int NumRecepcion { get; set; }
        public int CodProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public float Subtotal { get; set; }
        public int NumBodega { get; set; }
        public DetalleRecepcionEntidad()
        {
        }

        public DetalleRecepcionEntidad(int numRecepcion, int codProducto, string nombreProducto, int cantidad, float subtotal, int numBodega)
        {
            NumRecepcion = numRecepcion;
            CodProducto = codProducto;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            Subtotal = subtotal;
            NumBodega = numBodega;
        }
    }
}
