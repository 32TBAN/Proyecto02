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
        public static ConsultasEntidad DevolverConsulta(DateTime fechaInicio, DateTime fechaFin)
        {
            return ConsultaDatos.DevolverConsulta(fechaInicio,fechaFin);
        }
    }
}
