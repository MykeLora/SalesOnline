using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sales.Application.Dtos.Category;
using System.Text;
using Sales.Web.Models.Category;
using Sales.Application.Contract;
using Sales.Web.Services.Contract;
using Sales.Application.Models.Category;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Sales.Web.Controllers
{
    public class CategoryController : Controller
    {
        

        HttpClientHandler httpClientHandler = new HttpClientHandler();
        private readonly ICategoryServices categoryService;

        public CategoryController(ICategoryServices categoryService )
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {

            var categoryResult = await this.categoryService.Get();

            if (!categoryResult.Success)
            {
                ViewBag.Message = categoryResult.Message;
                return View(new List<CategoryGetModel>());
            }

             return View(categoryResult.Data);
        }



        public async Task<IActionResult> Details(int id)
        {
            var categoryResult = await this.categoryService.GetById(id);

            if (!categoryResult.Success)
            {
                ViewBag.Message = categoryResult.Message;
                return View();
            }

            return View(categoryResult.Data);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDtoAdd categoryDtoAdd)
        {
            categoryDtoAdd.UserId = 1;
            categoryDtoAdd.ChangeDate = DateTime.Now;

            if (!ModelState.IsValid){
                return View(categoryDtoAdd);
            }

            var result = await this.categoryService.Save(categoryDtoAdd);
            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View(categoryDtoAdd);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var result = await this.categoryService.GetById(id);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDtoUpdate categoryUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryUpdateDto);
            }

            var result = await this.categoryService.Update(categoryUpdateDto);

            if (!result.Success)
            {
                return View(categoryUpdateDto);
            }

            return RedirectToAction(nameof(Index));
        }



    }


}
