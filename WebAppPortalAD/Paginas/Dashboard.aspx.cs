using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppPortalAD.Controles;

using WebAppPortalAD.AD;
using CapaEntidad;
using CapaNegocio;
using System.Configuration;

namespace WebAppPortalAD.Paginas
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region Variables
        protected NegCRedireccionamientoLogin GenLogin = new NegCRedireccionamientoLogin();
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            GenLogin.RedireccionarALogin(this);
            if (Session["usuarioPro"].ToString() != null && Session["IdPreguntas"].ToString() != null && Session["memberof"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    if(Session["IdPreguntas"].ToString()=="0" && Session["memberof"].ToString() == "")
                    {
                        Response.Redirect("~/Paginas/ActualizarUsuario.aspx");
                    }
                }
            }
        }
        #endregion
    }
}