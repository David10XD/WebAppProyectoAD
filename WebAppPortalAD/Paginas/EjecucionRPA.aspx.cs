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
    public partial class EjecucionRPA : System.Web.UI.Page
    {

        #region Variables
        protected NegCRedireccionamientoLogin GenLogin = new NegCRedireccionamientoLogin();
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region GuardarAuditoriaProceso
        [WebMethod]
        public static string GuardarAuditoriaProceso(string IdEjecucionproceso, string IdProceso, string NombreProceso, string Estado,string Tipo)
        {
            string datos = null;
            try
            {
                EAuditoriaProceso eAuditoriaProceso = new EAuditoriaProceso();
                eAuditoriaProceso.Id = 0;
                eAuditoriaProceso.IdEjecucionproceso = Convert.ToInt32(IdEjecucionproceso);
                eAuditoriaProceso.IdProceso = Convert.ToInt32(IdProceso);
                eAuditoriaProceso.NombreProceso = Convert.ToString(NombreProceso);
                if (Estado == "0")
                {
                    eAuditoriaProceso.Estado = Convert.ToBoolean(0);
                }
                else if (Estado == "1")
                {
                    eAuditoriaProceso.Estado = Convert.ToBoolean(1);
                }
                eAuditoriaProceso.Tipo = Convert.ToInt32(Tipo);

                ERespuesta eRespuesta = NAuditoriaProceso.InsertarModificarEliminarAuditoriaProceso(eAuditoriaProceso);
                datos = eRespuesta.mensaje;

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