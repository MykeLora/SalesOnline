using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Dtos.TDocumentVenta
{
    public class TDocumentDtoBase
    {
        public int IdTDocument {  get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaRegistro {  get; set; }
        public string? Description { get; internal set; }
        public int IdUsuarioCreacion { get; internal set; }
        public DateTime ChangeDate { get; internal set; }
        public bool? esActivo {  get; set; }
    }
}
