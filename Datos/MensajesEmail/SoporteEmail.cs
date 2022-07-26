using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.MensajesEmail
{
    public class SoporteEmail : ServicioCorreo
    {
        public SoporteEmail()
        {
            correoRemitente = "proyectoesteban845@gmail.com";
            contraseña = "mzfpkzwliisfhunu";
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            iniciarSmtpClient();
        }
    }
}
