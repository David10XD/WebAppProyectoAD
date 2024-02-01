using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppPortalAD.AD;
using CapaEntidad;
using CapaNegocio;
using System.Configuration;

using System.Web.Services;
using System.Web.Script.Serialization;
using WebAppPortalAD.Clases;

using System.IO;

namespace WebAppPortalAD.Paginas
{
    public partial class CambiarPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region RecuperarContrasenia
        [WebMethod]
        public static string RecuperarContrasenia(string usuario, string cadena1, string cadena2)
        {
            string Respuesta = "";
            try
            {
                if ( cadena1 !=  cadena2)
                {
                    Respuesta = "Por favor ingresar la misma contraseña";
                }
                else
                {

                    string[] words = HttpContext.Current.Session["usuarioPro"].ToString().Split('@'); //usuario.Split('@');
                    string val = "";
                    if (ConfigurationManager.AppSettings["EstadoProceso"].ToString() == "1")
                    {
                        val = HttpContext.Current.Session["usuarioPro"].ToString();
                    }
                    else
                    {
                        val = words[0]; //usuario.Value.Replace("@indoamerica.edu.ec", "");
                    }

                    ActualizarCuentaAD actualizarCuenta = new ActualizarCuentaAD();
                    string resul = "";
                    resul = actualizarCuenta.CambiarContrasenia(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), val, cadena1, cadena2, cadena2, Convert.ToInt32(ConfigurationManager.AppSettings["EstadoProceso"].ToString()));
                    if (resul == "")
                    { }
                    else
                    {
                        if (resul == "Contraseña actualizada.")
                        {
                            EPreguntas ePreguntas = new EPreguntas();
                            ePreguntas.Usuario = val;
                            ePreguntas.Contrasenia = cadena1;
                            ePreguntas.Tipo = 0;
                            ERespuesta respuesta = NPreguntas.ActualizarUsuario(ePreguntas);
                            if (respuesta.mensaje == "Datos Guardados con Exito.")
                            {
                                EnvioCorreoHelper envioCorreo = new EnvioCorreoHelper();
                                envioCorreo.EnvioCorreo(val, "Actualización de contraseña", "");
                            }
                            Respuesta = resul;
                        }
                        else
                        {
                            Respuesta = "La Contraseña debe incluir caracteres de complejidad, es decir letras mayúsculas, minúsculas, número y caracteres especiales";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta = ex.Message.ToString();
            }
            return Respuesta;
        }
        #endregion

    }
}