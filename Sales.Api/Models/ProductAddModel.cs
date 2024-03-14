namespace Sales.Api.Models
{
    public class ProductAddModel
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }    
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
