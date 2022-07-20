using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ListaConsulta1
    {
        public string Fecha { get; set; }
        public float Monto { get; set; }
        public ListaConsulta1()
        {
                
        }

        public ListaConsulta1(string fecha, float monto)
        {
            Fecha = fecha;
            Monto = monto;
        }
    }
}
