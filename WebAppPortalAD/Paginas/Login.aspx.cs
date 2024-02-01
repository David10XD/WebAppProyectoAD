using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppPortalAD.AD;
using WebAppPortalAD.ADServicios;
using CapaEntidad;
using CapaNegocio;
using System.Configuration;

using System.Web.Services;
using System.Web.Script.Serialization;
using WebAppPortalAD.Clases;

using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebAppPortalAD.Paginas
{
    public partial class Login : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;

            var valor = usuario.ClientID;
        }
        #endregion

        #region btn_ingresar_Click
        protected void btn_ingresar_Click(object sender, EventArgs e)
        {
            if (IsReCaptchValid())
            {
                Label1.Visible = false ;
                Label1.Text = "";
                string[] words = usuario.Value.Split('@');
                string val = "";
                if (ConfigurationManager.AppSettings["EstadoProceso"].ToString() == "1")
                {
                    val = usuario.Value; //usuario.Value.Replace("@indoamerica.edu.ec", "");
                }
                else
                {
                    val = words[0]; //usuario.Value.Replace("@indoamerica.edu.ec", "");
                }

                Session["usuarioPro"] = val;// usuario.Value.TrimEnd(charsToTrim);
                Session["clave"] = clave.Value;

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);

                SerWebApp app = new SerWebApp();
                string Resultado = "";
                try
                {
                    List<EValidarUsuario> Lista = null;
                    EValidarUsuario pasardata = new EValidarUsuario();
                    Resultado = app.ValidarCuenta("UsuarioAd", "U$uar102023", usuario.Value, clave.Value);
                    Lista = JsonConvert.DeserializeObject<List<EValidarUsuario>>(Resultado);
                    if(Lista.Count >= 1)
                    {
                        foreach (EValidarUsuario ee in Lista)
                        {
                            pasardata.Estado = ee.Estado;
                            pasardata.Perfil = ee.Perfil;
                            pasardata.Memberof = ee.Memberof;
                            pasardata.IntentosIngreso = ee.IntentosIngreso;
                            pasardata.ContadorIntentos = ee.ContadorIntentos;
                            pasardata.EstadoBool = ee.EstadoBool;
                            pasardata.CambiarClaveAlInicio = ee.CambiarClaveAlInicio;
                            pasardata.NombreUsuario = ee.NombreUsuario;
                            pasardata.UnidadOU = ee.UnidadOU;
                            pasardata.Mensaje = ee.Mensaje;
                        }

                        if (pasardata.Estado == "66050" || pasardata.Estado == "66082" || pasardata.Estado == "514")
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                            Label1.Visible = true;
                            Label1.Text = "Su cuenta esta deshabilitada";
                        }
                        else if ((pasardata.Estado == "512" || pasardata.Estado == "66048" || pasardata.Estado == "66080") && (pasardata.Mensaje == "Usuario encontrado!"))
                        {
                            Session["user"] = pasardata.NombreUsuario;
                            Session["pass"] = clave.Value;
                            Session["unidad"] = pasardata.UnidadOU;
                            List<EValidar> Lista2 = null;
                            EPreguntas ePreguntas = new EPreguntas();
                            SeguridadHelper seguridad = new SeguridadHelper();
                            ePreguntas.Contrasenia = clave.Value;
                            ePreguntas.Usuario = pasardata.NombreUsuario;
                            ePreguntas.IdDominio = "";
                            ePreguntas.Server = "";
                            List<EValidar> eEPreguntas = NValidar.ValidarRegistro(ePreguntas);
                            Lista2 = eEPreguntas;
                            foreach (EValidar ee in Lista2)
                            {
                                Session["IdPreguntas"] = ee.contador.ToString();
                            }
                            Session["memberof"] = pasardata.Memberof;
                            Label1.Visible = false;
                            Label1.Text = "";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", true);
                            Response.Redirect("~/Paginas/Dashboard.aspx", false);
                        }
                        else if (pasardata.Estado == "512" && (pasardata.Mensaje == "Usuario no encontrado!" || pasardata.Mensaje == "El nombre de usuario o contraseña son incorrectos") )
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                            Label1.Visible = true;
                            Label1.Text = pasardata.Mensaje;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                        Label1.Visible = true;
                        Label1.Text = "No se encuentra el usuario, favor comunicarse con el administrador";
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                    Label1.Visible = true;
                    Label1.Text = ex.Message.ToString();
                }

                //ActualizarCuentaAD actualizarCuenta = new ActualizarCuentaAD();
                //string Resultado = "";
                //bool CambiarContrasenia = false;
                //try
                //{

                //    List<EValidar> Lista = null;
                //    EPreguntas ePreguntas = new EPreguntas();
                //    SeguridadHelper seguridad = new SeguridadHelper();
                //    ePreguntas.Contrasenia = seguridad.Encripta(HttpContext.Current.Session["clave"].ToString());
                //    ePreguntas.Usuario = HttpContext.Current.Session["usuarioPro"].ToString().Replace(ConfigurationManager.AppSettings["Replace"].ToString(), "");
                //    ePreguntas.IdDominio = ConfigurationManager.AppSettings["dominio"].ToString();
                //    ePreguntas.Server = ConfigurationManager.AppSettings["server"].ToString();
                //    List<EValidar> eEPreguntas = NValidar.ValidarRegistro(ePreguntas);
                //    Lista = eEPreguntas;
                //    //Session["IdPreguntas"] = Lista.Count.ToString();
                //    string Clave = "";
                //    int contador = 0;
                //    bool banderaIngreso = false;

                //    foreach (EValidar ee in eEPreguntas)
                //    {
                //        Clave = ee.clave;
                //        contador = ee.contador;
                //        Session["IdPreguntas"] = contador;
                //        if (Clave != "")
                //        {
                //            Clave = seguridad.Desencripta(ee.clave);
                //        }
                //    }

                //    if (contador == 0)
                //    {
                //        banderaIngreso = false;
                //    }
                //    else if (contador != 0 && Clave != "")
                //    {
                //        if (Clave == clave.Value)
                //        {
                //            banderaIngreso = false;
                //        }
                //        else
                //        {
                //            banderaIngreso = true;
                //        }
                //    }

                //    Resultado = actualizarCuenta.ValidarCuenta(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), ConfigurationManager.AppSettings["usuario"].ToString(), ConfigurationManager.AppSettings["clave"].ToString(), ConfigurationManager.AppSettings["carpeta"].ToString(), val + ";" + clave.Value, Convert.ToInt32(ConfigurationManager.AppSettings["EstadoProceso"].ToString()));
                //    string[] datosDoc = Resultado.Split(';');
                //    VerErrores(datosDoc.Length.ToString(), "PruebasSistema", "PruebasSistema");
                //    VerErrores(Resultado.ToString(), "PruebasSistema", "PruebasSistema");

                //    if (datosDoc.Length == 5)
                //    {
                //        if (datosDoc[4].ToString() == "0")
                //        {
                //            CambiarContrasenia = true;
                //        }
                //        else
                //        {
                //            CambiarContrasenia = false;
                //        }
                //    }
                //    else if (datosDoc.Length == 8)
                //    {
                //        if (datosDoc[7].ToString() == "0")
                //        {
                //            CambiarContrasenia = true;
                //        }
                //        else
                //        {
                //            CambiarContrasenia = false;
                //        }
                //    }
                //    if (datosDoc.Length == 3)
                //    {
                //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                //        Label1.Visible = true;
                //        Label1.Text = "No se encuentra el usuario, favor comunicarse con el administrador";
                //    }
                //    else
                //    {
                //        if (CambiarContrasenia == false)
                //        {
                //            if (datosDoc.Length == 5)
                //            {
                //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                //                if (datosDoc[1] == "66050")
                //                {
                //                    Label1.Visible = true;
                //                    Label1.Text = "Su cuenta esta deshabilitada";
                //                }
                //                else if (datosDoc[1] == "66082")
                //                {
                //                    Label1.Visible = true;
                //                    Label1.Text = "Su cuenta esta deshabilitada";
                //                }
                //                else if (datosDoc[1] == "514")
                //                {
                //                    Label1.Visible = true;
                //                    Label1.Text = "Su cuenta esta deshabilitada";
                //                }
                //                else
                //                {
                //                    Label1.Visible = true;
                //                    Label1.Text = datosDoc[0].ToString().Replace(" -", "");
                //                }
                //            }
                //            else
                //            {
                //                if (Convert.ToInt32(datosDoc[6].ToString()) < 3)
                //                {
                //                    Session["memberof"] = datosDoc[4];
                //                    Session["grupo"] = datosDoc[5];
                //                    VerErrores("Session[grupo]: " + Session["grupo"].ToString(), "PruebasSistema", "PruebasSistema");

                //                    if (datosDoc[3].ToString() == "Existe la cuenta")
                //                    {
                //                        if (datosDoc[2].ToString() == "")
                //                        {
                //                            Session["usuarioInicio"] = usuario.Value;
                //                        }
                //                        else
                //                        {
                //                            Session["usuarioInicio"] = datosDoc[2].ToString();
                //                        }

                //                        if (datosDoc[0] == " Existe el usuario")
                //                        {

                //                            if (datosDoc[1] == "512" || datosDoc[1] == "66048" || datosDoc[1] == "66080")
                //                            {
                //                                Session["estado"] = datosDoc[1];
                //                                ActualizarCuentaAD FechaCaducidad = new ActualizarCuentaAD();
                //                                string fecha = FechaCaducidad.FechaCaducidad(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), Session["usuarioPro"].ToString(), Session["clave"].ToString());

                //                                DateTime fechaActual = DateTime.Now;

                //                                //int dias = DateTime.Compare( fechaActual, Convert.ToDateTime(fecha));
                //                                //if (dias <= 0)
                //                                //{
                //                                //    Label1.Text = "Su contraseña expiro por favor Actualizar..";
                //                                //}
                //                                //else
                //                                //{
                //                                Label1.Visible = false;
                //                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", true);

                //                                VerErrores("usuario: " + Session["usuarioPro"].ToString(), "log", "log");
                //                                VerErrores("IdPreguntas: " + Session["IdPreguntas"].ToString(), "log", "log");
                //                                VerErrores("memberof: " + Session["memberof"].ToString(), "log", "log");

                //                                Response.Redirect("~/Paginas/Dashboard.aspx", false);
                //                                //Server.Transfer("Dashboard.aspx");
                //                                //}
                //                            }
                //                            else if (datosDoc[1] == "66050")
                //                            {
                //                                Label1.Visible = true;
                //                                Label1.Text = "La cuenta está deshabilitada";
                //                            }
                //                            else if (datosDoc[1] == "66082")
                //                            {
                //                                Label1.Visible = true;
                //                                Label1.Text = "La cuenta está deshabilitada";
                //                            }
                //                            else if (datosDoc[1] == "514")
                //                            {
                //                                Label1.Visible = true;
                //                                Label1.Text = "Su cuenta esta deshabilitada";
                //                            }
                //                            else
                //                            {
                //                                Label1.Visible = true;
                //                                Label1.Text = "Su cuenta esta Bloqueado..";
                //                            }
                //                        }
                //                        else
                //                        {
                //                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                //                            Label1.Visible = true;
                //                            Label1.Text = datosDoc[0];
                //                        }
                //                    }
                //                    else if (datosDoc[3].ToString().Replace("\r\n", "") == "The user name or password is incorrect.")
                //                    {
                //                        string detalle = "";
                //                        if (Convert.ToInt32(datosDoc[6].ToString()) == 0)
                //                        {
                //                            detalle = "El nombre de usuario o contraseña son incorrectos, te quedan dos intento";
                //                        }
                //                        else if (Convert.ToInt32(datosDoc[6].ToString()) == 1)
                //                        {
                //                            detalle = "El nombre de usuario o contraseña son incorrectos, te quedan un intento";
                //                        }
                //                        else if (Convert.ToInt32(datosDoc[6].ToString()) == 2 || Convert.ToInt32(datosDoc[6].ToString()) == 3)
                //                        {
                //                            detalle = "tu cuenta esta bloqueada comuniquese con el administrador";
                //                            Session["bloqueo"] = detalle;
                //                        }

                //                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                //                        Label1.Visible = true;
                //                        Label1.Text = detalle;
                //                    }
                //                }
                //                else if (Convert.ToInt32(datosDoc[6].ToString()) == 3)
                //                {
                //                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                //                    Label1.Visible = true;
                //                    Session["bloqueo"] = "tu cuenta esta bloqueada comuniquese con el administrador";
                //                    Label1.Text = "tu cuenta esta bloqueada comuniquese con el administrador";
                //                }
                //                else if (datosDoc[2].ToString() == "Administrator")
                //                {
                //                    Session["memberof"] = datosDoc[4];
                //                    Session["grupo"] = datosDoc[5];

                //                    if (datosDoc[3].ToString() == "Existe la cuenta")
                //                    {
                //                        if (datosDoc[2].ToString() == "")
                //                        {
                //                            Session["usuarioInicio"] = usuario.Value;
                //                        }
                //                        else
                //                        {
                //                            Session["usuarioInicio"] = datosDoc[2].ToString();
                //                        }

                //                        if (datosDoc[0] == " Existe el usuario")
                //                        {

                //                            if (datosDoc[1] == "512" || datosDoc[1] == "66048" || datosDoc[1] == "66080")
                //                            {
                //                                Session["estado"] = datosDoc[1];
                //                                ActualizarCuentaAD FechaCaducidad = new ActualizarCuentaAD();
                //                                string fecha = FechaCaducidad.FechaCaducidad(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), Session["usuarioPro"].ToString(), Session["clave"].ToString());

                //                                DateTime fechaActual = DateTime.Now;

                //                                //int dias = DateTime.Compare( fechaActual, Convert.ToDateTime(fecha));
                //                                //if (dias <= 0)
                //                                //{
                //                                //    Label1.Text = "Su contraseña expiro por favor Actualizar..";
                //                                //}
                //                                //else
                //                                //{
                //                                Label1.Visible = false;
                //                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", true);
                //                                Response.Redirect("~/Paginas/Dashboard.aspx");
                //                                //}
                //                            }
                //                            else if (datosDoc[1] == "66050")
                //                            {
                //                                Label1.Visible = true;
                //                                Label1.Text = "Su cuenta esta deshabilitada";
                //                            }
                //                            else if (datosDoc[1] == "66082")
                //                            {
                //                                Label1.Visible = true;
                //                                Label1.Text = "Su cuenta esta deshabilitada";
                //                            }
                //                            else if (datosDoc[1] == "514")
                //                            {
                //                                Label1.Visible = true;
                //                                Label1.Text = "Su cuenta esta deshabilitada";
                //                            }
                //                            else
                //                            {
                //                                Label1.Visible = true;
                //                                Label1.Text = "Su cuenta esta Bloqueado..";
                //                            }
                //                        }
                //                        else
                //                        {
                //                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                //                            Label1.Visible = true;
                //                            Label1.Text = datosDoc[0];
                //                        }
                //                    }
                //                    else if (datosDoc[3].ToString().Replace("\r\n", "") == "The user name or password is incorrect.")
                //                    {
                //                        string detalle = "";
                //                        if (Convert.ToInt32(datosDoc[6].ToString()) == 0)
                //                        {
                //                            detalle = "El nombre de usuario o contraseña son incorrectos, te quedan dos intento";
                //                        }
                //                        else if (Convert.ToInt32(datosDoc[6].ToString()) == 1)
                //                        {
                //                            detalle = "El nombre de usuario o contraseña son incorrectos, te quedan un intento";
                //                        }
                //                        else if (Convert.ToInt32(datosDoc[6].ToString()) == 2 || Convert.ToInt32(datosDoc[6].ToString()) == 3)
                //                        {
                //                            detalle = "tu cuenta esta bloqueada comuniquese con el administrador";
                //                            Session["bloqueo"] = detalle;
                //                        }

                //                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                //                        Label1.Visible = true;
                //                        Label1.Text = detalle;
                //                    }
                //                }
                //            }
                //        }
                //        else
                //        {
                //            Response.Redirect("~/Paginas/CambiarPassword.aspx");
                //        }
                //    }

                //}
                //catch (Exception ex)
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide();", false);
                //    Label1.Visible = true;
                //    Label1.Text = ex.Message.ToString();

                //}
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Validación Captcha incorrecta";
            }
        }
        #endregion

        #region btnRecuperar_Click
        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (usuario.Value == "" && txtPassword2.Value == "" && txtPassword3.Value == "")
            //    {
            //        Label2.Text = "Por favor ingresar el Usuario y la contraseña";
            //        Label2.Visible = true;
            //    }
            //    else
            //    {

            //        string[] words = usuario.Value.Split('@');
            //        string val = words[0]; //usuario.Value.Replace("@indoamerica.edu.ec", "");

            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup1();", true);
            //        ActualizarCuentaAD actualizarCuenta = new ActualizarCuentaAD();
            //        Label2.Text = actualizarCuenta.CambiarContrasenia(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), val, txtPassword2.Value, txtPassword3.Value, txtPassword3.Value);
            //        if (Label2.Text == "")
            //        { }
            //        else
            //        {
            //            if(Label2.Text == "Contraseña actualizada.")
            //            {
            //                EPreguntas ePreguntas = new EPreguntas();
            //                ePreguntas.Usuario = val;
            //                ePreguntas.Contrasenia = txtPassword3.Value;
            //                ePreguntas.Tipo = 0;
            //                ERespuesta respuesta = NPreguntas.ActualizarUsuario(ePreguntas);
            //                Label2.Visible = false;
            //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide1();", true);
            //                Response.Redirect("https://credenciales.indoamerica.edu.ec/");
            //            }
            //            else
            //            {
            //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide1();", false);
            //                Label2.Visible = true;
            //                Label2.Text = "Debe ingresar una clave con letras Mayusculas y minusculas, un caracter especial $ @";
            //            }
            //        }
            //    }
            //}

            //catch (Exception ex)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide1();", false);
            //    Label2.Visible = true;
            //    Label2.Text = ex.Message.ToString();

            //}        
        }
        #endregion

        #region RecuperarContrasenia
        [WebMethod]
        public static string RecuperarContrasenia(string usuario, string cadena1, string cadena2)
        {
            string Respuesta = "";
            try
            {
                //if (HttpContext.Current.Session["bloqueo"].ToString() == "tu cuenta esta bloqueada comuniquese con el administrador")
                //{
                //    Respuesta = HttpContext.Current.Session["bloqueo"].ToString();
                //}
                //else
                //{
                if (usuario == "" || cadena1 == "" || cadena2 == "")
                {
                    Respuesta = "Por favor ingresar el Usuario y la contraseña";
                    //Label2.Visible = true;
                }
                else
                {

                    string[] words = usuario.Split('@');
                    string val = ""; //usuario.Value.Replace("@indoamerica.edu.ec", "");

                    if (ConfigurationManager.AppSettings["EstadoProceso"].ToString() == "1")
                    {
                        val = usuario; //usuario.Value.Replace("@indoamerica.edu.ec", "");
                    }
                    else
                    {
                        val = words[0]; //usuario.Value.Replace("@indoamerica.edu.ec", "");
                    }

                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup1();", true);
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
                                envioCorreo.EnvioCorreo(usuario, "Actualización de contraseña", "");
                            }
                            Respuesta = resul;
                        }
                        else
                        {
                            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopupHide1();", false);
                            //Label2.Visible = true;
                            Respuesta = "La Contraseña debe incluir caracteres de complejidad, es decir letras mayúsculas, minúsculas, número y caracteres especiales";
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                Respuesta = ex.Message.ToString();
            }
            return Respuesta;
        }
        #endregion

        #region ConsultarPreguntas
        [WebMethod]
        public static List<EPreguntas> ConsultarPreguntas(string Usuario,string IdPreguntas,string pregunta, int Tipo)
        {
            List<EPreguntas> Lista = null;

            SeguridadHelper seguridad = new SeguridadHelper();
            string[] words = Usuario.Split('@');
            string val = words[0]; //usuario.Value.Replace("@indoamerica.edu.ec", "");

            try
            {
                List<EPreguntas> eEPreguntas = NPreguntas.ConsultarPreguntas(val, Convert.ToInt32(IdPreguntas), pregunta, Convert.ToInt32(Tipo));
                Lista = eEPreguntas;
             
            }
            catch (Exception ex)
            {
                Lista = new List<EPreguntas>();
                throw ex;
            }

            return Lista;
        }
        #endregion

        //Método para validar Captcha
        #region IsReCaptchValid
        public bool IsReCaptchValid()
        {
            var result = false;
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = ConfigurationManager.AppSettings["secrekey"];
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }
            return result;
        }
        #endregion

        #region ConsultarRespuesta
        [WebMethod]
        public static List<EPreguntas> ConsultarRespuesta(string Usuario, string IdPreguntas, string pregunta, int Tipo)
        {
            List<EPreguntas> Lista = null;

            SeguridadHelper seguridad = new SeguridadHelper();
            string[] words = Usuario.Split('@');
            string val = words[0]; //usuario.Value.Replace("@indoamerica.edu.ec", "");

            try
            {
                List<EPreguntas> eEPreguntas = NPreguntas.ConsultarPreguntas(val, Convert.ToInt32(IdPreguntas), pregunta, Convert.ToInt32(Tipo));
                //Lista = eEPreguntas;
                Lista = new List<EPreguntas>();
                foreach (EPreguntas Lista2 in eEPreguntas)
                {
                    EPreguntas usuario = new EPreguntas();
                    usuario.Respuestas = seguridad.Desencripta(Lista2.Respuestas);
                    Lista.Add(usuario);
                }


            }
            catch (Exception ex)
            {
                Lista = new List<EPreguntas>();
                throw ex;
            }

            return Lista;
        }
        #endregion

        #region desbloquerUsuario
        [WebMethod]
        public static string desbloquerUsuario(string usuario)
        {
            string datos = null;
            try
            {
                ActualizarCuentaAD actualizarCuenta = new ActualizarCuentaAD();
                datos = actualizarCuenta.DesbloquearUsuario(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), ConfigurationManager.AppSettings["usuario"].ToString(), ConfigurationManager.AppSettings["clave"].ToString(), ConfigurationManager.AppSettings["carpeta"].ToString(), usuario);

                if (datos == "")
                { }
                else
                {
                    datos = "1;" + datos;
                    //HttpContext.Current.Response.Redirect("Login.aspx");
                }
            }
            catch (Exception ex)
            {
                datos = ex.Message.ToString();
                throw ex;
            }

            return datos;
        }
        #endregion

        #region VerErrores
        public void VerErrores(string valor, string Carpeta, string rucEmpresa)
        {
            try
            {
                string fecha;
                fecha = DateTime.Now.ToString("dd-MM-yyyy");//DateTime.Now.ToShortDateString().Replace("/", "-");
                if (!Directory.Exists(@"C:\\" + rucEmpresa + "\\" + Carpeta + "\\" + fecha))
                {
                    Directory.CreateDirectory(@"C:\\" + rucEmpresa + "\\" + Carpeta + "\\" + fecha);
                }

                string path = @"C:\\" + rucEmpresa + "\\" + Carpeta + "\\" + fecha + "\\log.txt";
                TextWriter tw = new StreamWriter(path, true);
                tw.WriteLine("A fecha de : " + DateTime.Now.ToString() + ": " + valor);
                tw.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Exception: " + ex.Message);
            }
        }
        #endregion

    }
}