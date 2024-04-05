using Newtonsoft.Json;
using Sales.Application.Core;
using Sales.Application.Dtos.Product;
using Sales.Application.Dtos.TDocumentVenta;
using Sales.Application.Models.Product;
using Sales.Application.Models.TDocumentVentas;
using Sales.Web.Services.Contract;
using System.Text;

namespace Sales.Web.Services.ServicesEntities
{
    public class TDocuementServices : ITdocumentVentaSevices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public TDocuementServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "http://localhost:5158/api/TDocumentVenta/";
        }

        public async Task<ServicesResult<IEnumerable<TDocumentVentaGetModel>>> Get()
        {
            var result = new ServicesResult<IEnumerable<TDocumentVentaGetModel>>();

            using (var response = await _httpClient.GetAsync(_baseUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result.Data = JsonConvert.DeserializeObject<IEnumerable<TDocumentVentaGetModel>>(apiResponse);
                }
                else
                {
                    result.Success = false;
                    result.Message = "Error obtenido los TdocumentVenta";
                }
            }

            return result!;
        }

        public async Task<ServicesResult<TDocumentVentaGetModel>> GetById(int id)
        {
            var result = new ServicesResult<TDocumentVentaGetModel>();

            using (var response = await _httpClient.GetAsync(_baseUrl + $"GetTDocumentById?id={id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result.Data = JsonConvert.DeserializeObject<TDocumentVentaGetModel>(apiResponse);
                }
                else
                {
                    result.Success = false;
                    result.Message = "Error obtenido el TDocumentVenta";
                }
            }

            return result!;
        }

        public async Task<ServicesResult<bool>> Save(TDocumentDtoAdd add)
        {
            var result = new ServicesResult<bool>();


            StringContent content = new StringContent(JsonConvert.SerializeObject(add), Encoding.UTF8, "application/json");


            using (var response = await _httpClient.PostAsync(_baseUrl + "Save TDocumentVenta", content))
            {
                result.Data = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = "Error registrando el TdocumentVenta";
                }
            }

            return result;
        }

        public async Task<ServicesResult<bool>> Update(TDocumentDtoUpdate update)
        {
            var result = new ServicesResult<bool>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(_baseUrl + "UpdateTDocumentVenta", content))
            {
                result.Data = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = "Error actualizando el TdocumentVenta.";
                }
            }

            return result;
        }

    }
}
