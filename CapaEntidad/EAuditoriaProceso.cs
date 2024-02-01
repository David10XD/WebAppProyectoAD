using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EAuditoriaProceso
    {
        public int Id { get; set; }
        public int IdEjecucionproceso { get; set; }
        public int IdProceso { get; set; }
        public string NombreProceso { get; set; }
        public bool Estado { get; set; }
        public Int32 Tipo { get; set; }
    }
}
