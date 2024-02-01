using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EUsuario
    {
        public Int64 IdUsuarioPortal { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Identificacion { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Compania { get; set; }
        public string Departamento { get; set; }
        public string Titulo { get; set; }
        public string Contrasenia { get; set; }
        public string Portal { get; set; }
        public string Unidad { get; set; }
        public string Estado { get; set; }
        public int Tipo { get; set; }
    }
}
