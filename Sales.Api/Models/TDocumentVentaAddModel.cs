namespace Sales.Api.Models
{
    public class TDocumentVentaAddModel
    {
        public int? TDocumentVentaId { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
