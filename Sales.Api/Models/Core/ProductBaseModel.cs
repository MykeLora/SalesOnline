using Sales.Api.Models.Core;

namespace Sales.Api.Models
{
    public class ProductBaseModel :  ModelBase
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }    
        public decimal? Price { get; set; }
        public string? Description { get; set; }

    }
}
