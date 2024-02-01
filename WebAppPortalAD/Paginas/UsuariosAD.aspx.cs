using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppPortalAD.AD;
using System.Configuration;
using CapaEntidad;
using CapaNegocio;

using System.Web.Services;
using System.Web.Script.Serialization;

namespace WebAppPortalAD.Paginas
{
    public partial class UsuariosAD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region ListaUsuarios
        [WebMethod]
        public static string ListaUsuarios(string respuesta)
        {
            string datos = null;
            try
            {
                ActualizarCuentaAD ListaUsuario = new ActualizarCuentaAD();
                datos = ListaUsuario.ListarUsuarios(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), HttpContext.Current.Session["usuarioPro"].ToString(), HttpContext.Current.Session["clave"].ToString(), ConfigurationManager.AppSettings["carpeta"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["EstadoProceso"].ToString()));

            }
            catch (Exception ex)
            {
                datos = ex.Message.ToString();
                throw ex;
            }

            return datos;
        }
        #endregion
    }
}