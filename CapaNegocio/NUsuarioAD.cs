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
    public class NUsuarioAD
    {
        #region GetConsultarUsuarioAD
        public static List<EUsuariosAD> GetConsultarUsuarioAD(Int64 IdUsuarioAd, int Tipo)
        {
            return ADUsuarioAD.GetConsultarUsuarioAD(IdUsuarioAd, Tipo);
        }
        #endregion
    }
}
