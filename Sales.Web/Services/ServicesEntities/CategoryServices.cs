using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sales.Application.Core;
using Sales.Application.Dtos.Category;
using Sales.Application.Models.Category;
using Sales.Application.Models.TDocumentVentas;
using Sales.Web.Models.Category;
using Sales.Web.Services.Contract;
using System.Text;
using System.Text.Json;

namespace Sales.Web.Services.ServicesEntities
{
    public class CategoryServices : ICategoryServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CategoryServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "http://localhost:5158/api/Category/";
        }

        public async Task<ServicesResult<IEnumerable<CategoryGetModel>>> Get()
        {
            var result = new ServicesResult<IEnumerable<CategoryGetModel>>();

            using (var response = await _httpClient.GetAsync(_baseUrl + "GetCategories"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result.Data = JsonConvert.DeserializeObject<IEnumerable<CategoryGetModel>>(apiResponse);
                }
                else
                {
                    result.Success = false;
                    result.Message = "Error obtenido las categorias";
                }
            }

            return result!;
        }

        public async Task<ServicesResult<CategoryGetModel>> GetById(int id)
        {
            var result = new ServicesResult<CategoryGetModel>();

            using (var response = await _httpClient.GetAsync(_baseUrl + $"GetCategoryById?id={id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result.Data = JsonConvert.DeserializeObject<CategoryGetModel>(apiResponse);
                }
                else
                {
                    result.Success = false;
                    result.Message = "Error obtenido las categorias";
                }
            }

            return result!;
        }

        public async Task<ServicesResult<bool>> Save(CategoryDtoAdd add)
        {
            var result = new ServicesResult<bool>();


            StringContent content = new StringContent(JsonConvert.SerializeObject(add), Encoding.UTF8, "application/json");


            using (var response = await _httpClient.PostAsync(_baseUrl + "SaveCategory", content))
            {
                result.Data = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = "Error agregando la categoria";
                }
            }

            return result;
        }

        public async Task<ServicesResult<bool>> Update(CategoryDtoUpdate update)
        {
            var result = new ServicesResult<bool>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(_baseUrl + "UpdateCategory", content))
            {
                result.Data = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = "Error actualizando la categoria.";
                }
            }

            return result;
        }

    }
}
