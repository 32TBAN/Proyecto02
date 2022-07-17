using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class VentaEntidad
    {
        public int Num { get; set; }
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public float Total { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public VentaEntidad()
        {

        }
        public VentaEntidad(int num, int idUsuario, int idCliente, float total, DateTime fecha, bool estado)
        {
            Num = num;
            IdUsuario = idUsuario;
            IdCliente = idCliente;
            Total = total;
            Fecha = fecha;
            Estado = estado;
        }
    }
}
