
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
using Newtonsoft.Json;
using WebAppPortalAD.Controles;

namespace WebAppPortalAD.Paginas
{
    public partial class DesbloquerUsuario : System.Web.UI.Page
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
                        Response.Redirect("~/Paginas/ActualizarUsuario.aspx");
                    }
                    else
                    {
                        Label1.Visible = false;
                        Label1.Text = "";
                        formfield1.Value = Session["usuarioPro"].ToString();
                    }
                }
            }
        }
        #endregion

        #region btn_guardar_Click
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            if(switchfield1.Checked == true || switchfield1.Checked == false)
            {
                ActualizarCuentaAD actualizarCuenta = new ActualizarCuentaAD();
                Label1.Text = actualizarCuenta.DesbloquearUsuario(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), Session["usuarioPro"].ToString(), Session["clave"].ToString(), ConfigurationManager.AppSettings["carpeta"].ToString(), Session["usuarioPro"].ToString());

                if (Label1.Text == "")
                { }
                else
                {
                    Label1.Visible = true;
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Label1.Text = "La cuenta no a sido desbloqueada";
                Label1.Visible = true;
            }
        }
        #endregion

        #region btn_cancelar_Click
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region ListaUsuarios
        [WebMethod]
        public static string  ValidarEstado(string respuesta)
        {
            string Lista = "";
            try
            {

                Lista = Convert.ToString(HttpContext.Current.Session["estado"].ToString());

            }
            catch (Exception ex)
            {
                Lista = "0";
                throw ex;
            }

            return Lista;
        }
        #endregion

    }
}