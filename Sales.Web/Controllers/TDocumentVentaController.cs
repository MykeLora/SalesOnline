using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sales.Application.Dtos.Category;
using Sales.Application.Dtos.Product;
using Sales.Application.Models.Product;
using Sales.Web.Models.Product;
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
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            List<ProductGetModel> products;

            using (var httpClient = new HttpClient(this.httpClientHandler))
            {
                var url = "http://localhost:5158/api/TDocumentVenta";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        products = JsonConvert.DeserializeObject<List<ProductGetModel>>(apiResponse);

                        if (products == null || products.Count == 0)
                        {
                            ViewBag.Message = "No se encontraron los productos.";
                            return View();
                        }
                    }
                    else
                    {
                        // Manejar el caso en el que la solicitud no fue exitosa
                        ViewBag.Message = "Error al obtener los productos desde la API.";
                        return View();
                    }
                }
            }

            return View(products);
        }



        public async Task<IActionResult> Details(int id)
        {
            var product = new ProductDetailView();

            using (var httpClient = new HttpClient(this.httpClientHandler))
            {
                var url = $"http://localhost:5158/api/TDocumentVenta/GetTDocumentById?id={id}";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<ProductDetailView>(apiResponse);

                        if (!product.success)
                        {
                            ViewBag.Message = product.message;
                            return View();
                        }
                    }
                }
            }

            if (product.data == null)
            {
                // Aquí puedes manejar el caso en que no se encontró ninguna categoría con el id proporcionado
                ViewBag.Message = "No se encontró ninguna categoría con el ID proporcionado.";
                return View();
            }

            return View(product.data);
        }


        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDtoAdd categoryDtoAdd)
        {
            try
            {
                using (var httpClient = new HttpClient(this.httpClientHandler))
                {
                    var url = $"http://localhost:5158/api/TDocumentVenta/Save";

                    categoryDtoAdd.UserId = 1;
                    categoryDtoAdd.ChangeDate = DateTime.Now;

                    StringContent content = new StringContent(JsonConvert.SerializeObject(categoryDtoAdd), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();

                            //category = JsonConvert.DeserializeObject<CategoryDetailView>(apiResponse);


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
            var product = new ProductDetailView();

            using (var httpClient = new HttpClient(this.httpClientHandler))
            {
                var url = $"http://localhost:5158/api/Product/GetProductById?id={id}";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<ProductDetailView>(apiResponse);

                        if (!product.success)
                        {
                            ViewBag.Message = product.message;
                            return View();
                        }
                    }


                }
            }

            return View(product.data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductsDtoUpdate productsDtoUpdate)
        {
            try
            {
                using (var httpClient = new HttpClient(this.httpClientHandler))
                {
                    var url = $"http://localhost:5158/api/TDocumentVenta/UpdateTDocumentVenta";

                    productsDtoUpdate.UserId = 1;
                    productsDtoUpdate.ChangeDate = DateTime.Now;

                    StringContent content = new StringContent(JsonConvert.SerializeObject(productsDtoUpdate), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            //category = JsonConvert.DeserializeObject<CategoryDetailView>(apiResponse);

                            //if (!category.success)
                            //{
                            //    ViewBag.Message = category.message;
                            //   return View();
                            //}
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
