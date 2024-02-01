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
    public class NValidar
    {
        #region ValidarRegistro
        public static List<EValidar> ValidarRegistro(EPreguntas objPregunta)
        {
            return ADValidar.ValidarRegistro(objPregunta);
        }
        #endregion
    }
}
