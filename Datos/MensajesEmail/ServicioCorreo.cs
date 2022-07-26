using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Datos.MensajesEmail
{
    public abstract class ServicioCorreo
    {

        private SmtpClient smtpClient;
        public String correoRemitente { get; set; }
        public String contraseña { get; set; }
        public String host { get; set; }
        public int port { get; set; }
        public bool ssl { get; set; }

        protected void iniciarSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(correoRemitente, contraseña);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
        }
        public void sendMail(string sujeto, string boby, List<string> recipienteMail)
        {
            var mensaje = new MailMessage();
            try
            {
                mensaje.From = new MailAddress(correoRemitente);
                foreach (var mail in recipienteMail)
                {
                    mensaje.To.Add(mail);
                }
                mensaje.Subject = sujeto;
                mensaje.Body = boby;
                mensaje.Priority = MailPriority.Normal;
                smtpClient.Send(mensaje);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                mensaje.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}
