using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EParametrosCorreo
    {
        public string smtpAddress { get; set; }
        public string emailFrom { get; set; }
        public string password { get; set; }
        public int portNumber { get; set; }
        public bool enableSSL { get; set; }
        public string emailFromName { get; set; }
        public string Mensaje { get; set; }
        public int tipo { get; set; }

    }
}
