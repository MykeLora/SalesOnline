using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sales.Application.Dtos.Category;
using Sales.Application.Models.Category;
using Sales.Web.Models.Category;
using System.Net.Http;
using System.Security.Policy;
using System.Text;

namespace Sales.Web.Controllers
{
    public class CategoryController : Controller
    {
        HttpClientHandler httpClientHandler = new HttpClientHandler();

        public CategoryController()
        {
            this.httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyError) => { return true; };

        }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            List<CategoryGetModel> categories;

            using (var httpClient = new HttpClient(this.httpClientHandler))
            {
                var url = "http://localhost:5158/api/Category/GetCategories";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        categories = JsonConvert.DeserializeObject<List<CategoryGetModel>>(apiResponse);

                        if (categories == null || categories.Count == 0)
                        {
                            ViewBag.Message = "No se encontraron categorías.";
                            return View();
                        }
                    }
                    else
                    {
                        // Manejar el caso en el que la solicitud no fue exitosa
                        ViewBag.Message = "Error al obtener las categorías desde la API.";
                        return View();
                    }
                }
            }

            return View(categories);
        }



        // GET: CategoryController/Details/5
        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = new CategoryDetailView();

            using (var httpClient = new HttpClient(this.httpClientHandler))
            {
                var url = $"http://localhost:5158/api/Category/GetCategoryById?id={id}";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        category = JsonConvert.DeserializeObject<CategoryDetailView>(apiResponse);

                        if (!category.success)
                        {
                            ViewBag.Message = category.message;
                            return View();
                        }
                    }
                }
            }

            if (category.data == null)
            {
                // Aquí puedes manejar el caso en que no se encontró ninguna categoría con el id proporcionado
                ViewBag.Message = "No se encontró ninguna categoría con el ID proporcionado.";
                return View();
            }

            return View(category.data);
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
                    var url = $"http://localhost:5158/api/Category/SaveCategory";

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

        // GET: CategoryController/Edit/5
        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = new CategoryDetailView();

            using (var httpClient = new HttpClient(this.httpClientHandler))
            {
                var url = $"http://localhost:5158/api/Category/GetCategoryById?id={id}";

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        category = JsonConvert.DeserializeObject<CategoryDetailView>(apiResponse);

                        if (!category.success)
                        {
                            ViewBag.Message = category.message;
                            return View();
                        }
                    }


                }
            }

            return View(category.data);
        }
        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDtoUpdate categoryUpdteDto)
        {
            try
            {
                using (var httpClient = new HttpClient(this.httpClientHandler))
                {
                    var url = $"http://localhost:5158/api/Category/UpdateCategory";

                    categoryUpdteDto.UserId = 1;
                    categoryUpdteDto.ChangeDate = DateTime.Now;

                    StringContent content = new StringContent(JsonConvert.SerializeObject(categoryUpdteDto), Encoding.UTF8, "application/json");

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
