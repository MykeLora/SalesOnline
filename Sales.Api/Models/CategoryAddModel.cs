namespace Sales.Api.Models
{
     public class CategoryAddModel
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
