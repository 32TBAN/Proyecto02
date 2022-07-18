using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ConsultasEntidad
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int NumDias { get; set; }
        public int NumClientes { get;  set; }
        public int NumProductores { get;  set; }
        public int NumProductos { get;  set; }
        public int NumVentas { get; set; }
        public float TotalIngresos { get; set; }
        public float TotalBeneficio { get; set; }
        public ConsultasEntidad()
        {

        }

    }
}
