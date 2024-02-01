using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EPreguntas
    {
        public Int32 Tipo { get; set; }
        public Int32 IdProceso { get; set; }
        public Int32 IdPreguntas { get; set; }
        public Int32 IdUsuario { get; set; }

        public string Usuario { get; set; }

        public string IdDominio { get; set; }
        public string Server { get; set; }
        public string Respuestas { get; set; }

        public string Contrasenia { get; set; }

        public string Descripcion { get; set; }
        public string Contenedor { get; set; }
        public string Titulo { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string Compania { get; set; }
        public string Departamento { get; set; }
        public string Telefono { get; set; }
    }
}
