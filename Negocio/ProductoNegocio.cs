using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Negocio
{
    public static class ProductoNegocio
    {
        public static ProductosEntidad Guardar(ProductosEntidad productosEntidad)
        {
            return ProductoDatos.Guardar(productosEntidad);
        }

        public static List<ProductosEntidad> DevolverProductos()
        {
            return ProductoDatos.DevolverProductos();
        }

        public static ProductosEntidad BuscarProducto(int id)
        {
            return ProductoDatos.BuscarProducto(id);
        }

        public static bool EliminarProducto(int id)
        {
            return ProductoDatos.EliminarProducto(id);
        }
    }
}
