using Newtonsoft.Json;
using Sales.Application.Core;
using Sales.Application.Dtos.Category;
using Sales.Application.Dtos.Product;
using Sales.Application.Models.Category;
using Sales.Application.Models.Product;
using Sales.Application.Models.TDocumentVentas;
using Sales.Domain.Entites;
using Sales.Web.Models.Category;
using Sales.Web.Models.Product;
using Sales.Web.Services.Contract;
using Sales.Web.Services.Core;
using System.Text;
using System.Text.Json;

namespace Sales.Web.Services.ServicesEntities
{
    public class ProductServices : IProductServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "http://localhost:5158/api/Product/";
        }

        public async Task<ServicesResult<IEnumerable<ProductGetModel>>> Get()
        {
            var result = new ServicesResult<IEnumerable<ProductGetModel>>();

            using (var response = await _httpClient.GetAsync(_baseUrl + "GetProducts"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result.Data = JsonConvert.DeserializeObject<IEnumerable<ProductGetModel>>(apiResponse);
                }
                else
                {
                    result.Success = false;
                    result.Message = "Error obtenido los productos";
                }
            }

            return result!;
        }

        public async Task<ServicesResult<ProductGetModel>> GetById(int id)
        {
            var result = new ServicesResult<ProductGetModel>();

            using (var response = await _httpClient.GetAsync(_baseUrl + $"GetProductById?id={id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result.Data = JsonConvert.DeserializeObject<ProductGetModel>(apiResponse);
                }
                else
                {
                    result.Success = false;
                    result.Message = "Error obtenido el producto";
                }
            }

            return result!;
        }

        public async Task<ServicesResult<bool>> Save(ProductsDtoAdd add)
        {
            var result = new ServicesResult<bool>();


            StringContent content = new StringContent(JsonConvert.SerializeObject(add), Encoding.UTF8, "application/json");


            using (var response = await _httpClient.PostAsync(_baseUrl + "SaveProduct", content))
            {
                result.Data = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = "Error registrando el producto";
                }
            }

            return result;
        }

        public async Task<ServicesResult<bool>> Update(ProductsDtoUpdate update)
        {
            var result = new ServicesResult<bool>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(_baseUrl + "UpdateProduct", content))
            {
                result.Data = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = "Error actualizando el producto.";
                }
            }

            return result;
        }

    }
}
