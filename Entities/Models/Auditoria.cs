using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Auditoria
    {
        public int AuditoriaId { get; set; }
        public int UsuarioId { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }
        public string Entidad { get; set; }
        public int? EntidadId { get; set; }
        public string Antes { get; set; }
        public string Despues { get; set; }

    }
}
