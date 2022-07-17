using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public static class ClienteNegocio
    {
        public static ClienteEntidad Guardar(ClienteEntidad clienteEntidad)
        {
            if (clienteEntidad.Id == 0)
            {
                return ClienteDatos.Guardar(clienteEntidad);
            }
            else
            {
                return ClienteDatos.Actualizar(clienteEntidad);
            }
        }

        public static ClienteEntidad DevolverClienteCedula(string text)
        {
            return ClienteDatos.DevolverClienteCedula(text);
        }
    }
}
