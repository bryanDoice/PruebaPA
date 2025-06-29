using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ArticuloDTO
    {
        public int ArticuloId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string Ubicacion { get; set; }
        public int CategoriaId { get; set; }
    }
}
