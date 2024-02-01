using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EUsuariosAD
    {
        public Int64 IdUsuarioAd { get; set; }
        public string Servidor { get; set; }
        public string Dominio { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public int TipoLogueo { get; set; }
        public int Estado { get; set; }
        public int Tipo { get; set; }
    }
}
