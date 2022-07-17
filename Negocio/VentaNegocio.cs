using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public static class VentaNegocio
    {
        public static VentaEntidad ComenzarVenta(VentaEntidad ventaEntidad)
        {
            if (ventaEntidad.Num == 0)
            {
                return VentaDatos.Guardar(ventaEntidad);
            }
            else
            {
                return VentaDatos.Actualizar(ventaEntidad);
            }
        }

        public static DetalleVentaEntidad AgregarDetalle(DetalleVentaEntidad detalleVentaEntidad, bool v)
        {
            if (v)
            {
                return VentaDatos.GuardarDetalle(detalleVentaEntidad);
            }
            else
            {
                return VentaDatos.ActualizarDetalle(detalleVentaEntidad);
            }
        }

        public static List<DetalleVentaEntidad> DevolverDetalle(int num)
        {
            return VentaDatos.DevolverDetalle(num);
        }
    }
}
