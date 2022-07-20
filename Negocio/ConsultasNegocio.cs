using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Negocio
{
    public static class ConsultasNegocio
    {
        public static ConsultasEntidad DevolverConsulta(ConsultasEntidad consultasEntidad)
        {
            return ConsultaDatos.DevolverConsulta(consultasEntidad);
        }

        public static List<ListaConsulta1> ConsultaMontoFecha(DateTime fechaInicio, DateTime fechaFin, int numDias)
        {
            return ConsultaDatos.ConsultaMontoFecha(fechaInicio, fechaFin,numDias);
        }

        public static List<ConsultaStockProductos> ConsultaStockFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return ConsultaDatos.ConsultaStockFecha(fechaInicio,fechaFin);
        }

        public static List<ConsultaTopProductos> ConsultaTopProductos(DateTime fechaInicio, DateTime fechaFin)
        {
            return ConsultaDatos.ConsultaTopProductos(fechaInicio, fechaFin);
        }
    }
}
