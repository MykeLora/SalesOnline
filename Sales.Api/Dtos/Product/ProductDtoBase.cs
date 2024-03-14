namespace Sales.Api.Dtos.Product
{
    public class ProductDtoBase : DtoBase
    {
        public string? Marca { get; set; }
        public decimal? Precio { get; set; }
        public string? Descripcion { get; set; }
        public int? Stock { get; set; } = 0;
        public int? IdCategoria { get; set; }
    }
}
