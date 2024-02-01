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
using WebAppPortalAD.Controles;
using WebAppPortalAD.Clases;
using Newtonsoft.Json;

namespace WebAppPortalAD.Paginas
{
    public partial class ActualizarUsuario : System.Web.UI.Page
    {
        #region Variables
        protected NegCRedireccionamientoLogin GenLogin = new NegCRedireccionamientoLogin();
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            GenLogin.RedireccionarALogin(this);
            if (Session["usuarioPro"] != null && Session["IdPreguntas"] != null && Session["memberof"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    if (Session["IdPreguntas"].ToString() == "0" && Session["memberof"].ToString() == "0")
                    {
                        //Label1.Visible = false;
                        //Label1.Text = "";
                        //formfield1.Value = Session["usuarioPro"].ToString();

                        //ActualizarCuentaAD FechaCaducidad = new ActualizarCuentaAD();
                        //string fecha = FechaCaducidad.FechaCaducidad(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), Session["usuarioPro"].ToString(), Session["clave"].ToString());
                       // formfieldFecha.Value = fecha;
                    }
                    else
                    {
                        //Label1.Visible = false;
                        //Label1.Text = "";
                        //formfield1.Value = Session["usuarioPro"].ToString();

                        //ActualizarCuentaAD FechaCaducidad = new ActualizarCuentaAD();
                        //string fecha = FechaCaducidad.FechaCaducidad(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), Session["usuarioPro"].ToString(), Session["clave"].ToString());
                        //formfieldFecha.Value = fecha;
                    }
                }
            }
        }
        #endregion

        #region btn_guardar_Click
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            if (formfield3.Value == "" && formfield4.Value == "")
            {
                Label1.Text = "Por favor ingresar el Usuario y la contraseña";
                Label1.Visible = true;
            }
            else
            {
                
                ActualizarCuentaAD actualizarCuenta = new ActualizarCuentaAD();
                string resul = "";
                resul = actualizarCuenta.CambiarContrasenia(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), Session["usuarioPro"].ToString(), Session["clave"].ToString(), formfield3.Value, formfield4.Value, Convert.ToInt32(ConfigurationManager.AppSettings["EstadoProceso"].ToString()));
                if (resul == "")
                { }
                else
                {
                    if (resul == "Contraseña actualizada.")
                    {
                        SeguridadHelper seguridad = new SeguridadHelper();
                        EPreguntas ePreguntas = new EPreguntas();
                        ePreguntas.Usuario = Session["usuarioPro"].ToString();
                        ePreguntas.Contrasenia = seguridad.Encripta(formfield3.Value);
                        ePreguntas.Tipo = 0;
                        ERespuesta respuesta = NPreguntas.ActualizarUsuario(ePreguntas);
                        Label1.Text = resul;
                        Label1.Visible = true;
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        Label1.Text = "La Contraseña debe incluir caracteres de complejidad, es decir letras mayúsculas, minúsculas, número y caracteres especiales";
                        Label1.Visible = true;
                    }
                }
            }
        }
        #endregion

        #region btn_cancelar_Click
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        #endregion

        #region consultapreguntas
        [WebMethod]
        public static List<EPreguntas> consultapreguntas()
        {
            List<EPreguntas> Lista =new List<EPreguntas>();

            try
            {
                EPreguntas ePreguntas = new EPreguntas();
                SeguridadHelper seguridad = new SeguridadHelper();
                ePreguntas.Contrasenia = seguridad.Encripta(HttpContext.Current.Session["clave"].ToString());
                ePreguntas.Usuario = HttpContext.Current.Session["user"].ToString(); //HttpContext.Current.Session["usuarioPro"].ToString().Replace(ConfigurationManager.AppSettings["Replace"].ToString(), "");
                ePreguntas.IdDominio = ConfigurationManager.AppSettings["dominio"].ToString();
                ePreguntas.Server = ConfigurationManager.AppSettings["server"].ToString();
                List<EPreguntas> eEPreguntas = NPreguntas.RespuestaUsuario(ePreguntas);
                foreach ( EPreguntas eEPregunta in eEPreguntas)
                {
                    EPreguntas subpregun = new EPreguntas();
                    subpregun.IdProceso = eEPregunta.IdProceso;
                    subpregun.IdPreguntas = eEPregunta.IdPreguntas;
                    subpregun.Descripcion = eEPregunta.Descripcion;
                    subpregun.Respuestas = seguridad.Desencripta(eEPregunta.Respuestas);
                    Lista.Add(subpregun);
                }
                    //Lista = eEPreguntas;
            }
            catch (Exception ex)
            {
                Lista = new List<EPreguntas>();
                throw ex;
            }

            return Lista;
        }
        #endregion

        #region Existepreguntas
        [WebMethod]
        public static List<EPreguntas> Existepreguntas()
        {
            List<EPreguntas> Lista = null;

            try
            {
                EPreguntas ePreguntas = new EPreguntas();
                SeguridadHelper seguridad = new SeguridadHelper();
                ePreguntas.Contrasenia = seguridad.Encripta(HttpContext.Current.Session["clave"].ToString());
                ePreguntas.Usuario = HttpContext.Current.Session["usuarioPro"].ToString().Replace(ConfigurationManager.AppSettings["Replace"].ToString(), "");
                ePreguntas.IdDominio = ConfigurationManager.AppSettings["dominio"].ToString();
                ePreguntas.Server = ConfigurationManager.AppSettings["server"].ToString();
                List<EPreguntas> eEPreguntas = NPreguntas.RespuestaUsuario(ePreguntas);
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

        #region MostrarPreguntas
        [WebMethod]
        public static List<EPreguntas> MostrarPreguntas()
        {
            List<EPreguntas> Lista = null;

            try
            {
                List<EPreguntas> eEPreguntas = NPreguntas.MostrarPreguntas();
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

        #region GuardarPreguntas
        [WebMethod]
        public static string GuardarPreguntas(string preguntas)
        {
            string datos=null;
            string[] enca = preguntas.Split('-');
            List<User> Listas = null;
            try
            {
                EPreguntas ePreguntas = new EPreguntas();
                SeguridadHelper seguridad = new SeguridadHelper();
                string Titulo = ""; 
                string Email = "";
                string NombreCompleto = "";
                string Compania = "";
                string Departamento = "";
                string Telefono = "";
                //string datosUsuario = ConfigurationManager.AppSettings["carpeta"].ToString();
                //string[] rutaBusqueda = HttpContext.Current.Session["grupo"].ToString().Split('↨');
                //datosUsuario = "LDAP://" + rutaBusqueda[1];
                ////VerErrores(datosUsuario.ToString(), "PruebasSistema", "PruebasSistema");
                //ActualizarCuentaAD ListaUsuario = new ActualizarCuentaAD();
                //datos = ListaUsuario.ListarUsuarios(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), HttpContext.Current.Session["usuarioPro"].ToString(), HttpContext.Current.Session["clave"].ToString(), datosUsuario, Convert.ToInt32(ConfigurationManager.AppSettings["EstadoProceso"].ToString()));
                //Listas = JsonConvert.DeserializeObject<List<User>>(datos);

                SerWebApp app = new SerWebApp();
                datos = app.ListaUsuarios("UsuarioAd", "U$uar102023", HttpContext.Current.Session["user"].ToString(), HttpContext.Current.Session["pass"].ToString(), HttpContext.Current.Session["unidad"].ToString());
                Listas = JsonConvert.DeserializeObject<List<User>>(datos);

                foreach (User Lista in Listas)
                {
                    Titulo = Lista.JobTitle;
                    Email = Lista.Email;
                    NombreCompleto = Lista.DisplayName;
                    Compania = Lista.Company;
                    Departamento = Lista.Deparment;
                    Telefono = Lista.Phone;
                }

                foreach (string r in enca.ToArray())
                {
                    string[] enca1 = r.Split(';');
                    ePreguntas.IdProceso = 0;
                    ePreguntas.IdPreguntas = Convert.ToInt32(enca1[0]);
                    ePreguntas.IdDominio = "";// ConfigurationManager.AppSettings["dominio"].ToString();
                    ePreguntas.Server = "";// ConfigurationManager.AppSettings["server"].ToString();
                    ePreguntas.Usuario = HttpContext.Current.Session["user"].ToString();//HttpContext.Current.Session["usuarioPro"].ToString().Replace(ConfigurationManager.AppSettings["Replace"].ToString(), "");
                    ePreguntas.Contrasenia = seguridad.Encripta(HttpContext.Current.Session["clave"].ToString());
                    ePreguntas.Respuestas = seguridad.Encripta(Convert.ToString(enca1[1]));
                    ePreguntas.Contenedor = HttpContext.Current.Session["unidad"].ToString();
                    ePreguntas.Titulo = Titulo;
                    ePreguntas.Email = Email;
                    ePreguntas.NombreCompleto = NombreCompleto;
                    ePreguntas.Compania = Compania;
                    ePreguntas.Departamento = Departamento;
                    ePreguntas.Telefono = Telefono;
                    ePreguntas.Tipo = 1;

                    ERespuesta respuesta = NPreguntas.InsertarModificarEliminarRespuestaUsuarios(ePreguntas);
                    datos = respuesta.mensaje;
                    HttpContext.Current.Session["IdPreguntas"] = "1";
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

        #region ActualizarPreguntas
        [WebMethod]
        public static string ActualizarPreguntas(string preguntas)
        {
            //preguntas = "1;ARECHUA-2;LUIS-1;2";
            string datos = null;
            string[] enca = preguntas.Split('-');
            List<User> Listas = null;
            try
            {
                EPreguntas ePreguntas = new EPreguntas();
                SeguridadHelper seguridad = new SeguridadHelper();

                string Titulo = "";
                string Email = "";
                string NombreCompleto = "";
                string Compania = "";
                string Departamento = "";
                string Telefono = "";
                string datosUsuario = ConfigurationManager.AppSettings["carpeta"].ToString();
                string[] rutaBusqueda = HttpContext.Current.Session["grupo"].ToString().Split('↨');
                datosUsuario = "LDAP://" + rutaBusqueda[1];
                //VerErrores(datosUsuario.ToString(), "PruebasSistema", "PruebasSistema");
                ActualizarCuentaAD ListaUsuario = new ActualizarCuentaAD();
                datos = ListaUsuario.ListarUsuarios(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), HttpContext.Current.Session["usuarioPro"].ToString(), HttpContext.Current.Session["clave"].ToString(), datosUsuario, Convert.ToInt32(ConfigurationManager.AppSettings["EstadoProceso"].ToString()));
                Listas = JsonConvert.DeserializeObject<List<User>>(datos);

                foreach (User Lista in Listas)
                {
                    Titulo = Lista.JobTitle;
                    Email = Lista.Email;
                    NombreCompleto = Lista.DisplayName;
                    Compania = Lista.Company;
                    Departamento = Lista.Deparment;
                    Telefono = Lista.Phone;
                }

                foreach (string r in enca.ToArray())
                {
                    string[] enca1 = r.Split(';');
                    ePreguntas.IdProceso = Convert.ToInt32(enca1[2]); 
                    ePreguntas.IdPreguntas = Convert.ToInt32(enca1[0]);
                    ePreguntas.IdDominio = ConfigurationManager.AppSettings["dominio"].ToString();
                    ePreguntas.Server = ConfigurationManager.AppSettings["server"].ToString();
                    ePreguntas.Usuario = HttpContext.Current.Session["usuarioPro"].ToString().Replace(ConfigurationManager.AppSettings["Replace"].ToString(), "");
                    ePreguntas.Contrasenia = seguridad.Encripta(HttpContext.Current.Session["clave"].ToString());
                    ePreguntas.Respuestas = seguridad.Encripta(Convert.ToString(enca1[1]));
                    ePreguntas.Contenedor = rutaBusqueda[1];
                    ePreguntas.Titulo = Titulo;
                    ePreguntas.Email = Email;
                    ePreguntas.NombreCompleto = NombreCompleto;
                    ePreguntas.Compania = Compania;
                    ePreguntas.Departamento = Departamento;
                    ePreguntas.Telefono = Telefono;
                    ePreguntas.Tipo = 2;

                    ERespuesta respuesta = NPreguntas.InsertarModificarEliminarRespuestaUsuarios(ePreguntas);
                    datos = respuesta.mensaje;
                    HttpContext.Current.Session["IdPreguntas"] = "1";
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

        #region ConsultarPreguntas
        [WebMethod]
        public static List<EPreguntas> ConsultarPreguntas()
        {
            List<EPreguntas> Lista = null;

            try
            {
                List<EPreguntas> eEPreguntas = NPreguntas.MostrarPreguntas();
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

        #region ConsultarPreguntasCombos
        [WebMethod]
        public static string ConsultarPreguntasCombos()
        {
            List<ECombos> Lista = new List<ECombos>();

            try
            {
                Lista = NPreguntas.MostrarPreguntasCombo();
            }
            catch (Exception ex)
            {
                Lista = new List<ECombos>();
                throw ex;
            }
            
            return JsonConvert.SerializeObject(Lista, Formatting.Indented); 
        }
        #endregion

    }
}