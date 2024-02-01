using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppPortalAD.AD;

namespace WebAppPortalAD.Paginas
{
    public partial class Consultas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnRevisar_Click(object sender, EventArgs e)
        {
            ActualizarCuentaAD actualizarCuenta = new ActualizarCuentaAD();

            txtDatos.Text = actualizarCuenta.ExtraerDatosUsuario("srvdc01", "serviconta.int", "pruebas", "password.1");
            txtDatos.Text = actualizarCuenta.FechaCaducidad("srvdc01", "serviconta.int", "pruebas", "password.1");
            //txtDatos.Text = actualizarCuenta.ValidarCuenta("srvdc01", "serviconta.int", "pruebas", "password.1");
            //txtDatos.Text = actualizarCuenta.DesbloquearUsuario("srvdc01", "serviconta.int", "pruebas", "password.1");
            txtDatos.Text = actualizarCuenta.CambiarContrasenia("srvdc01", "serviconta.int", "pruebas", "password.1", "password.2", "password.2",1);

            // Clases.ProcesoAcceso proceso = new Clases.ProcesoAcceso();
            //string respuesta= proceso.ConsultarDatosUsuarios("");


        }

        protected void btnRevisar0_Click(object sender, EventArgs e)
        {

        }
    }
}