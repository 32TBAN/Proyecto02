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
            if (recepcionEntidad.NumRec == 0)
            {
                return RecepcionDatos.ComenzarRecepcion(recepcionEntidad);
            }
            else
            {
                return RecepcionDatos.ActualizarRecepcion(recepcionEntidad);
            }
        }

        public static DetalleRecepcionEntidad AgregarProductoDetalle(DetalleRecepcionEntidad detalleRecepcion1, bool v)
        {
            if (v)
            {
                return RecepcionDatos.AgregarProductoDetalle(detalleRecepcion1);
            }
            else
            {
                return RecepcionDatos.ActualizarProductoDetalle(detalleRecepcion1);
            }
        }

        public static List<DetalleRecepcionEntidad> DevolverProductosDetalle(int numRecepcion)
        {
            return RecepcionDatos.DevolverProductosDetalle(numRecepcion);
        }
    }
}
