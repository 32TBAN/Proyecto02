using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public static class BodegaNegocio
    {
        public static BodegaEntidad Guardar(BodegaEntidad bodegaEntidad)
        {
            return BodegaDatos.Guardar(bodegaEntidad);
        }

        public static List<BodegaEntidad> DevolverBodegas()
        {
            return BodegaDatos.DevolverBodegas();
        }

        public static BodegaEntidad BuscarBodega(int num)
        {
            return BodegaDatos.BuscarBodega(num);
        }

        public static bool EliminarBodega(int num)
        {
            return BodegaDatos.EliminarBodega(num);
        }
    }
}
