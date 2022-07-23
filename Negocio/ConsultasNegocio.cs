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

        public static List<ListaConsulta1> ConsultaMontoFecha(ConsultasEntidad consultasEntidad)
        {
            return ConsultaDatos.ConsultaMontoFecha(consultasEntidad);
        }

        public static List<ConsultaStockProductos> ConsultaStockFecha(ConsultasEntidad consultasEntidad)
        {
            return ConsultaDatos.ConsultaStockFecha(consultasEntidad);
        }

        public static List<ConsultaTopProductos> ConsultaTopProductos(ConsultasEntidad consultasEntidad)
        {
            return ConsultaDatos.ConsultaTopProductos(consultasEntidad);
        }
    }
}
