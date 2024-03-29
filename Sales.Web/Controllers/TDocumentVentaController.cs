using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sales.Application.Dtos.Category;
using Sales.Application.Dtos.Product;
using Sales.Application.Dtos.TDocumentVenta;
using Sales.Application.Models.Product;
using Sales.Application.Models.TDocumentVentas;
using Sales.Web.Models.Product;
using Sales.Web.Models.TDocumentVenta;
using System.Text;

namespace Sales.Web.Controllers
{
    public class TDocumentVentaController : Controller
    {

        HttpClientHandler httpClientHandler = new HttpClientHandler();

        public TDocumentVentaController()
        {
            this.httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyError) => { return true; };

        }
   
        public async Task<IActionResult> Index()
        {
            List<TDocumentVentaGetModel> Tdocuements;

            using (var httpClient = new HttpClient(this.httpClientHandler))
            {
                var url = "http://localhost:5158/api/TDocumentVenta";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Tdocuements = JsonConvert.DeserializeObject<List<TDocumentVentaGetModel>>(apiResponse);

                        if (Tdocuements == null || Tdocuements.Count == 0)
                        {
                            ViewBag.Message = "No se encontraron los documentos de ventas.";
                            return View();
                        }
                    }
                    else
                    {
                        // Manejar el caso en el que la solicitud no fue exitosa
                        ViewBag.Message = "Error al obtener los documentos desde la API.";
                        return View();
                    }
                }
            }

            return View(Tdocuements);
        }



        public async Task<IActionResult> Details(int id)
        {
            var Tdocuments = new TDocumentDetailView();

            using (var httpClient = new HttpClient(this.httpClientHandler))
            {
                var url = $"http://localhost:5158/api/TDocumentVenta/GetTDocumentById?id={id}";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Tdocuments = JsonConvert.DeserializeObject<TDocumentDetailView>(apiResponse);

                        if (!Tdocuments.success)
                        {
                            ViewBag.Message = Tdocuments.message;
                            return View();
                        }
                    }
                }
            }

            if (Tdocuments.data == null)
            {
                
                ViewBag.Message = "No se encontró ningun Documento con el ID proporcionado.";
                return View();
            }

            return View(Tdocuments.data);
        }


        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TDocumentDtoAdd documentDtoAdd)
        {
            try
            {
                using (var httpClient = new HttpClient(this.httpClientHandler))
                {
                    var url = $"http://localhost:5158/api/TDocumentVenta/Save";

                    documentDtoAdd.UserId = 1;
                    documentDtoAdd.ChangeDate = DateTime.Now;

                    StringContent content = new StringContent(JsonConvert.SerializeObject(documentDtoAdd), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();


                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var TDocument = new TDocumentDetailView();

            using (var httpClient = new HttpClient(this.httpClientHandler))
            {
                var url = $"http://localhost:5158/api/TDocumentVenta/GetTDocumentById?id={id}";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        TDocument = JsonConvert.DeserializeObject<TDocumentDetailView>(apiResponse);

                        if (!TDocument.success)
                        {
                            ViewBag.Message = TDocument.message;
                            return View();
                        }
                    }


                }
            }

            return View(TDocument.data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TDocumentDtoUpdate documentDtoUpdate)
        {
            try
            {
                using (var httpClient = new HttpClient(this.httpClientHandler))
                {
                    var url = $"http://localhost:5158/api/TDocumentVenta/UpdateTDocumentVenta";

                    documentDtoUpdate.UserId = 1;
                    documentDtoUpdate.ChangeDate = DateTime.Now;

                    StringContent content = new StringContent(JsonConvert.SerializeObject(documentDtoUpdate), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                           // Tdocument = JsonConvert.DeserializeObject<TDocumentDetailView>(apiResponse);

                        }


                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
