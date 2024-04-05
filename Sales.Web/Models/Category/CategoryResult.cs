namespace Sales.Web.Models.Category
{
    public class CategoryResult 
    {
       
        public int categoryId { get; set; }
        public string? description { get; set; }
        public string? name { get; set; }
        public DateTime creationDate { get; set; }
    }
}
