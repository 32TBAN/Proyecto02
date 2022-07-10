using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class BodegaEntidad
    {
        public int Num { get; set; }
        public string Ubicacion { get; set; }
        public int Capacidad { get; set; }
        public BodegaEntidad()
        {

        }

        public BodegaEntidad(int id, string ubicacion, int capacidad)
        {
            Num = id;
            Ubicacion = ubicacion;
            Capacidad = capacidad;
        }
    }
}
