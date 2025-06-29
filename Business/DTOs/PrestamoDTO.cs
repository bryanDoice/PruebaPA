using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class PrestamoDTO
    {
        public int PrestamoId { get; set; }
        public int ArticuloId { get; set; }
        public int UsuarioSolicitaId { get; set; }
        public int? UsuarioApruebaId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public string Estado { get; set; }
    }
}
