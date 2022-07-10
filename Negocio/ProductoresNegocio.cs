using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public static class ProductoresNegocio
    {
        public static ProductoresEntidad Guardar(ProductoresEntidad productoresEntidad)
        {
            if (productoresEntidad.Id == 0)
            {
               return ProductoresDatos.Guardar(productoresEntidad);
            }
            else
            {
                return ProductoresDatos.Actualizar(productoresEntidad);
            }
        }

        public static ProductoresEntidad DevolverProductorCedula(string text)
        {
            return ProductoresDatos.DevolverProductorCedula(text);
        }
    }
}
