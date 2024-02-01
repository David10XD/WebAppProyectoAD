using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGridMail;
using SendGridMail.Transport;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using CapaEntidad;
using CapaNegocio;

namespace WebAppPortalAD.Clases
{
    public class EnvioCorreoHelper
    {
        public string ErrorProceso;

        public bool EnvioCorreo(string correosDestinatarios, string correoTitulo, string estructuraContenidoCorreo)
        {

            EParametrosCorreo parametrosServidorCorreo = new EParametrosCorreo();
            bool respuestaEnvioCorreo = false;
            string contenidoCorreo = "";

            try
            {
                List<EParametrosCorreo> Lista = null;
                parametrosServidorCorreo.tipo = 0;
                Lista = NPreguntas.MostrarEnvioMail(parametrosServidorCorreo);
                foreach (var item in Lista)
                {
                    parametrosServidorCorreo.smtpAddress = item.smtpAddress;
                    parametrosServidorCorreo.emailFrom = item.emailFrom;
                    parametrosServidorCorreo.password = item.password;
                    parametrosServidorCorreo.portNumber = Convert.ToInt32(item.portNumber);
                    parametrosServidorCorreo.enableSSL = Convert.ToBoolean(item.enableSSL);
                    estructuraContenidoCorreo = item.Mensaje;
                    string[] words = correosDestinatarios.Split('@');
                    string val = words[0];
                    estructuraContenidoCorreo = estructuraContenidoCorreo.Replace("{Destinatario}", val).Replace("{Empresa}", "Universidad Indoamérica");
                }
                contenidoCorreo = estructuraContenidoCorreo;
                respuestaEnvioCorreo = EnviarCorreo(correosDestinatarios, correoTitulo, contenidoCorreo, parametrosServidorCorreo);

            }
            catch
            {
                respuestaEnvioCorreo = false;
            }

            return respuestaEnvioCorreo;

        }

        #region EnviarCorreo
        /*
         * Función que permite el envio de correo electrónico 
         * Requiere de la entidad EntParametrosCorreo de donde toma la configuración del servidor de correo.
         * Se pueden enviar copias a correos si en el parametro strMailAdress se envía los correos separados por punto y coma.
        */
        public bool EnviarCorreo(string correosDestinatarios, string correoTitulo, string correoContenido, EParametrosCorreo parametrosServidorCorreo)
        {
            bool Temp = false;

            string smtpAddress = parametrosServidorCorreo.smtpAddress;
            string emailFrom = parametrosServidorCorreo.emailFrom;
            string password = parametrosServidorCorreo.password;
            string subject = correoTitulo;
            string body = correoContenido;
            int portNumber = parametrosServidorCorreo.portNumber;
            bool enableSSL = parametrosServidorCorreo.enableSSL;
            string emailFromName = parametrosServidorCorreo.emailFromName;

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom, emailFromName);

                foreach (string correoIndividual in correosDestinatarios.Split(new Char[] { ';' }))
                {
                    if (correoIndividual != "")
                    {
                        mail.To.Add(correoIndividual);
                    }
                }
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    try
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                        Temp = true;
                    }
                    catch (Exception ex)
                    {
                        Temp = false;
                        ErrorProceso = ex.Message.ToString().Trim();
                    }
                }
            }


            return Temp;
        }
        #endregion
    }
}