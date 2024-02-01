using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppPortalAD.AD;
using System.Configuration;
using System.Windows.Forms;
namespace WebAppPortalAD.Paginas
{
    public partial class DetalleCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActualizarCuentaAD actualizarCuenta = new ActualizarCuentaAD();
            Label1.Text = "Usuario: "  + Session["usuarioPro"].ToString();
            Label2.Text = actualizarCuenta.ExtraerDatosUsuario(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), Session["usuarioPro"].ToString(), Session["clave"].ToString());
            //MessageBox.Show(Label2.Text);
        }
    }
}