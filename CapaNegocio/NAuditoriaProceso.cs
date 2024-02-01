using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocio
{
    public class NAuditoriaProceso
    {
        #region InsertarModificarEliminarAuditoriaProceso
        public static ERespuesta InsertarModificarEliminarAuditoriaProceso(EAuditoriaProceso auditoriaProceso)
        {
            ERespuesta respuesta = new ERespuesta();

            respuesta = ADAuditoriaProceso.InsertarModificarEliminarAuditoriaProceso(auditoriaProceso);

            return respuesta;
        }
        #endregion
    }
}
