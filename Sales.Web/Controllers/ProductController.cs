using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sales.Application.Contract;
using Sales.Application.Dtos.Category;
using Sales.Application.Dtos.Product;
using Sales.Application.Models.Category;
using Sales.Application.Models.Product;
using Sales.Web.Models;
using Sales.Web.Models.Product;
using Sales.Web.Services.Contract;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Web.Controllers
{
    public class ProductController : Controller
    {


        HttpClientHandler httpClientHandler = new HttpClientHandler();
        private readonly IProductServices productServices;

        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        public async Task<IActionResult> Index()
        {

            var Result = await this.productServices.Get();

            if (!Result.Success)
            {
                ViewBag.Message = Result.Message;
                return View(new List<ProductGetModel>());
            }

            return View(Result.Data);
        }


        public async Task<IActionResult> Details(int id)
        {
            var Result = await this.productServices.GetById(id);

            if (!Result.Success)
            {
                ViewBag.Message = Result.Message;
                return View();
            }

            return View(Result.Data);
        }



        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsDtoAdd productsDtoAdd)
        {
            productsDtoAdd.UserId = 1;
            productsDtoAdd.ChangeDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View(productsDtoAdd);
            }

            var result = await this.productServices.Save(productsDtoAdd);
            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View(productsDtoAdd);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var result = await this.productServices.GetById(id);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductsDtoUpdate productsDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productsDto);
            }

            var result = await this.productServices.Update(productsDto);

            if (!result.Success)
            {
                return View(productsDto);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
