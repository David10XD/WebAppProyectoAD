using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebAppTramaAD.ADProceso;
using System.Configuration;

namespace WebAppTramaAD
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ProcesoAD" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ProcesoAD.svc o ProcesoAD.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ProcesoAD : IProcesoAD
    {
        #region CrearUsuarios
        public string CrearUsuarios(string datosUsuarios)
        { string Respusta = "";

            ActualizarCuentaAD GuardarUsuario = new ActualizarCuentaAD();
            Respusta = GuardarUsuario.GenerarUsuarios(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), ConfigurationManager.AppSettings["usuario"].ToString(), ConfigurationManager.AppSettings["clave"].ToString(), datosUsuarios, ConfigurationManager.AppSettings["carpeta"].ToString());

            return Respusta;
        }
        #endregion

        #region ActualizarUsuarios
        public string ActualizarUsuarios(string datosUsuarios)
        {
            string Respusta = "";

            ActualizarCuentaAD GuardarUsuario = new ActualizarCuentaAD();
            Respusta = GuardarUsuario.ActualizarUsuarios(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), ConfigurationManager.AppSettings["usuario"].ToString(), ConfigurationManager.AppSettings["clave"].ToString(), datosUsuarios, ConfigurationManager.AppSettings["carpeta"].ToString());

            return Respusta;
        }
        #endregion

        #region ListaUsuarios
        public string ListaUsuarios()
        {
            string Respusta = "";

            ActualizarCuentaAD GuardarUsuario = new ActualizarCuentaAD();
            Respusta = GuardarUsuario.ListarUsuarios(ConfigurationManager.AppSettings["server"].ToString(), ConfigurationManager.AppSettings["dominio"].ToString(), ConfigurationManager.AppSettings["usuario"].ToString(), ConfigurationManager.AppSettings["clave"].ToString() ,ConfigurationManager.AppSettings["carpeta"].ToString());

            return Respusta;
        }
        #endregion
    }
}
