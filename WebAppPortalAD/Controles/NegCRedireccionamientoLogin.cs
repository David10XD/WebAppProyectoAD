using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebAppPortalAD.Controles
{
    public class NegCRedireccionamientoLogin
    {
        #region RedireccionarALogin
        public bool RedireccionarALogin(System.Web.UI.Page p)
        {
            //return true;
            if (p.Session["usuarioPro"] == null || p.Session["usuarioPro"].ToString() == "")
            {
                string strEstadoLogin = "";
                if (!p.IsPostBack)
                    strEstadoLogin = "Termino el tiempo de Sesión, por favor ingrese al sistema nuevamente.";
                else
                    strEstadoLogin = "Termino el tiempo de Sesión, por favor ingrese al sistema nuevamente.";

                p.Response.Redirect(ConfigurationManager.AppSettings["URL.SERVER"].ToString() + "?strEstadoLogin="
                                  + strEstadoLogin, true);
                return false;
            }
            else
            {
                return true;
                //validar en base de datos si el usuario logueado tiene permisos para acceder a esta página
            }
        }
        #endregion
    }
}