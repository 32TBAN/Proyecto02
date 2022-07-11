using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class RecepcionEntidad
    {
        public int NumRec { get; set; }
        public int IdUsuario { get; set; }
        public int IdProductor { get; set; }
        public float Total { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public RecepcionEntidad()
        {

        }

        public RecepcionEntidad(int numRec, int idUsuario, int idProctor, float total, DateTime fechaRecepcion)
        {
            NumRec = numRec;
            IdUsuario = idUsuario;
            IdProductor = idProctor;
            Total = total;
            FechaRecepcion = fechaRecepcion;
        }
    }
}
