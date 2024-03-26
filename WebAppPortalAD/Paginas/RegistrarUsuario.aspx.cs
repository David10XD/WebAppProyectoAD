using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppPortalAD.AD;
using WebAppPortalAD.ADServicios;
using System.Configuration;
using CapaEntidad;
using CapaNegocio;

using System.Web.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using WebAppPortalAD.Controles;

using System.IO;
using WebAppPortalAD.Clases;

namespace WebAppPortalAD.Paginas
{
    public partial class RegistrarUsuario : System.Web.UI.Page
    {
        #region Variables
        protected NegCRedireccionamientoLogin GenLogin = new NegCRedireccionamientoLogin();
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            string v = Id.ClientID;
            GenLogin.RedireccionarALogin(this);
            if (Session["usuarioPro"].ToString() != null && Session["IdPreguntas"].ToString() != null && Session["memberof"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    if (Session["IdPreguntas"].ToString() == "0" && Session["memberof"].ToString() == "")
                    {
                        Response.Redirect("~/Paginas/ActualizarUsuario.aspx");
                    }
                    else
                    {
                        string[] words = Session["memberof"].ToString().Split('@');
                        TextBox1.Text= words[1];
                        Id.Text = Session["memberof"].ToString();

                    }
                }
            }
        }
        #endregion

        #region GuardarUsuarios
        [WebMethod]
        public static string GuardarUsuarios(string preguntas)
        {
            string datos = null;
            try
            {
                string[] words = preguntas.ToString().Split(';');

                ActualizarCuentaAD GuardarUsuario = new ActualizarCuentaAD();
                datos = GuardarUsuario.GenerarUsuarios(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), HttpContext.Current.Session["usuarioPro"].ToString(), HttpContext.Current.Session["clave"].ToString(), preguntas, ConfigurationManager.AppSettings["carpeta"].ToString());
            
            }
            catch (Exception ex)
            {
                datos = ex.Message.ToString();
                throw ex;
            }
          
            return datos;
        }
        #endregion

        #region ActualizarUnidad
        [WebMethod]
        public static string ActualizarUnidad(string IdUO,string Estado)
        {
            string datos = null;
            try
            {
                int idEstado = 0;
                if (Estado == "INACTIVO")
                {
                    idEstado = 1;
                }

                ERespuesta respuesta = new ERespuesta();
                respuesta = NPreguntas.InsertarModificarEliminarUnidad(Convert.ToInt64(IdUO), idEstado, 2);
                datos = respuesta.mensaje;
            }
            catch (Exception ex)
            {
                datos = ex.Message.ToString();
                throw ex;
            }

            return datos;
        }
        #endregion

        #region ActualizarUsuarios
        [WebMethod]
        public static string ActualizarUsuarios(string preguntas)
        {
            string datos = null;
            try
            {
                EPreguntas ePreguntas = new EPreguntas();
                SeguridadHelper seguridad = new SeguridadHelper();

                ActualizarCuentaAD GuardarUsuario = new ActualizarCuentaAD();
                datos = GuardarUsuario.ActualizarUsuarios(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), ConfigurationManager.AppSettings["usuario"].ToString(), ConfigurationManager.AppSettings["clave"].ToString(), preguntas, ConfigurationManager.AppSettings["carpeta"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["EstadoProceso"].ToString()));


                string datosUsuario = ConfigurationManager.AppSettings["carpeta"].ToString();
                string[] rutaBusqueda = HttpContext.Current.Session["grupo"].ToString().Split('↨');
                string[] enca1 = preguntas.Split(';');
                ePreguntas.IdDominio = ConfigurationManager.AppSettings["dominio"].ToString();
                ePreguntas.Server = ConfigurationManager.AppSettings["server"].ToString();
                ePreguntas.Usuario = enca1[0];
                if (enca1[7]!="" && enca1[7] != "default")
                {
                    ePreguntas.Contrasenia = seguridad.Encripta(enca1[7].ToString());
                }
                ePreguntas.Respuestas = "";
                ePreguntas.Contenedor = rutaBusqueda[1];
                ePreguntas.Titulo = enca1[3];
                ePreguntas.Email = enca1[4];
                ePreguntas.NombreCompleto = ""; 
                ePreguntas.Compania = enca1[1];
                ePreguntas.Departamento = enca1[2]; 
                ePreguntas.Telefono = enca1[5];
                ePreguntas.Tipo = 3;

                ERespuesta respuesta = NPreguntas.InsertarModificarEliminarRespuestaUsuarios(ePreguntas);

            }
            catch (Exception ex)
            {
                datos = ex.Message.ToString();
                throw ex;
            }

            return datos;
        }
        #endregion

        #region ListaUsuarios
        [WebMethod]
        public static List<User> ListaUsuarios(string respuesta)
        {
            string datos = null;
            List<User> Lista = null;
            try
            {

                if (HttpContext.Current.Session["memberof"].ToString() == "")
                {
                    SerWebApp app = new SerWebApp();
                    datos = app.ListaUsuarios("UsuarioAd", "U$uar102023", HttpContext.Current.Session["user"].ToString(), HttpContext.Current.Session["pass"].ToString(), HttpContext.Current.Session["unidad"].ToString());
                    Lista = JsonConvert.DeserializeObject<List<User>>(datos);
                }
                else
                {
                    SerWebApp app = new SerWebApp();
                    datos = app.ListaUsuarios("UsuarioAd", "U$uar102023", HttpContext.Current.Session["user"].ToString(), HttpContext.Current.Session["pass"].ToString(), HttpContext.Current.Session["unidad"].ToString());
                    Lista = JsonConvert.DeserializeObject<List<User>>(datos);
                }
            }
            catch (Exception ex)
            {
                Lista = new List<User>();
                //throw ex;
            }

            return Lista;
        }
        #endregion

        #region ListaUnidad
        [WebMethod]
        public static List<EListaOU> ListaUnidad(string respuesta)
        {
            string datos = null;
            List<EListaOU> Lista = null;
            try
            {
                if (HttpContext.Current.Session["memberof"].ToString() != "")
                {
                    SerWebApp app = new SerWebApp();
                    datos = app.ListaOU("UsuarioAd", "U$uar102023", "", "");
                    NPreguntas.InsertarModificarEliminarRegistroUO(datos, 1, 1);
                    Lista = NPreguntas.ConsultarUnidad(0, 0);
                }
            }
            catch (Exception ex)
            {
                Lista = new List<EListaOU>();
            }
            return Lista;
        }
        #endregion

        #region ListaUsuarioPorContenedor
        [WebMethod]
        public static List<User> ListaUsuarioPorContenedor(string respuesta, string respuesta2)
        {
            string datos = null;
            List<User> Lista = null;
            List<EListaOU> Lista2 = null;
            try
            {
                if (HttpContext.Current.Session["memberof"].ToString() == "")
                {
                    SerWebApp app = new SerWebApp();
                    Lista2 = NPreguntas.ConsultarUnidad(Convert.ToInt64(respuesta), 1);
                    foreach (EListaOU ee in Lista2)
                    {
                        respuesta2 = ee.Unidad;
                    }
                    datos = app.ListaUsuarios("UsuarioAd", "U$uar102023", "", "", respuesta2);
                    Lista = JsonConvert.DeserializeObject<List<User>>(datos);
                }
                else if (HttpContext.Current.Session["memberof"].ToString() != "")
                {
                    SerWebApp app = new SerWebApp();
                    Lista2 = NPreguntas.ConsultarUnidad(Convert.ToInt64(respuesta), 1);
                    foreach (EListaOU ee in Lista2)
                    {
                        respuesta2 = ee.Unidad;
                    }
                    datos = app.ListaUsuarios("UsuarioAd", "U$uar102023", "", "", respuesta2);
                    Lista = JsonConvert.DeserializeObject<List<User>>(datos);
                }
            }
            catch (Exception ex)
            {
                Lista = new List<User>();
                throw ex;
            }

            return Lista;
        }
        #endregion

        #region BuscarUsuarioPorContenedor
        [WebMethod]
        public static List<User> BuscarUsuarioPorContenedor(string respuesta, string respuesta2)
        {
            string datos = null;
            List<User> Lista = null;
            List<EListaOU> Lista2 = null;
            try
            {
                if (respuesta2 == "Seleccione el grupo")
                {
                    respuesta2 = "";
                }
                else
                {
                    Lista2 = NPreguntas.ConsultarUnidadNombre(respuesta2, 1);
                    foreach (EListaOU ee in Lista2)
                    {
                        respuesta2 = ee.Unidad;
                    }
                }
                if (HttpContext.Current.Session["memberof"].ToString() == "")
                {
                    SerWebApp app = new SerWebApp();
                    datos = app.ListaUsuarios("UsuarioAd", "U$uar102023", respuesta, "", respuesta2);
                    Lista = JsonConvert.DeserializeObject<List<User>>(datos);
                }
                else if (HttpContext.Current.Session["memberof"].ToString() != "")
                {
                    SerWebApp app = new SerWebApp();
                    datos = app.ListaUsuarios("UsuarioAd", "U$uar102023", respuesta, "", respuesta2);
                    Lista = JsonConvert.DeserializeObject<List<User>>(datos);             
                }
            }
            catch (Exception ex)
            {
                Lista = new List<User>();
                throw ex;
            }

            return Lista;
        }
        #endregion

        #region ConsultarUnidadCombos
        [WebMethod]
        public static string ConsultarUnidadCombos()
        {
            List<ECombos> Lista = new List<ECombos>();

            try
            {
                Lista = NPreguntas.MostrarUnidadCombo(0,2);
            }
            catch (Exception ex)
            {
                Lista = new List<ECombos>();
                throw ex;
            }

            return JsonConvert.SerializeObject(Lista, Formatting.Indented);
        }
        #endregion

        #region ActivarInactivarUsuario
        [WebMethod]
        public static string ActivarInactivarUsuario(string usuario,string tipo)
        {
            string datos = null;
            try
            {
                SerWebApp app = new SerWebApp();
                if (tipo == "1")
                {
                    datos = app.BloquerCuenta("UsuarioAd", "U$uar102023", usuario, "");
                }
                else if(tipo == "2")
                {
                    datos = app.DesbloquearCuenta("UsuarioAd", "U$uar102023", usuario, "");
                }
            }
            catch (Exception ex)
            {

            }
            return datos;
        }
        #endregion

        #region VerErrores
        public static void VerErrores(string valor, string Carpeta, string rucEmpresa)
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