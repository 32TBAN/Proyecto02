using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public static class RecepcionNegocio
    {
        public static RecepcionEntidad ComenzarRecepcion(RecepcionEntidad recepcionEntidad)
        {
            return RecepcionDatos.ComenzarRecepcion(recepcionEntidad);
        }

        public static bool AgregarProductoDetalle(List<DetalleRecepcionEntidad> detalleRecepcion)
        {
            return RecepcionDatos.AgregarProductoDetalle(detalleRecepcion);
        }

        public static List<DetalleRecepcionEntidad> DevolverProductosDetalle(int numRecepcion)
        {
            return RecepcionDatos.DevolverProductosDetalle(numRecepcion);
        }
    }
}
