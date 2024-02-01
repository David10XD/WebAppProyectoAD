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
    public class NApikeySistema
    {
        #region InsertarModificarEliminarApikeySistema
        public static ERespuesta InsertarModificarEliminarApikeySistema(EApikeySistema apikeySistema)
        {
            ERespuesta respuesta = new ERespuesta();

            respuesta = ADApikeySistema.InsertarModificarEliminarApikeySistema(apikeySistema);

            return respuesta;
        }
        #endregion
    }
}
