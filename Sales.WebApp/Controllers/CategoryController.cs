using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Contract;
using Sales.Application.Dtos.Category;
using Sales.Application.Models.Category;

namespace Sales.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            var result = this.categoryService.GetAll();

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            List<CategoryGetModel> categories = result.Data;
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var result = this.categoryService.Get(id);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryDtoAdd categoryDtoAdd)
        {
            try
            {
                categoryDtoAdd.ChangeDate = DateTime.Now;
                categoryDtoAdd.UserId = 1;

                this.categoryService.Save(categoryDtoAdd);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = this.categoryService.Get(id);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }
            return View(result.Data);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryDtoUpdate categoryDtoUpdate)
        {
            try
            {
                categoryDtoUpdate.ChangeDate = DateTime.Now;
                categoryDtoUpdate.UserId = categoryDtoUpdate.UserId;

                this.categoryService.Update(categoryDtoUpdate);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

 
    }
}
