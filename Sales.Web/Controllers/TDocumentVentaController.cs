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
using Sales.Web.Services.Contract;
using System.Text;

namespace Sales.Web.Controllers
{
    public class TDocumentVentaController : Controller
    {

        HttpClientHandler httpClientHandler = new HttpClientHandler();
        private readonly ITdocumentVentaSevices tdocumentVenta;

        public TDocumentVentaController(ITdocumentVentaSevices tdocumentVenta)
        {
            this.tdocumentVenta = tdocumentVenta;
        }

        public async Task<IActionResult> Index()
        {

            var Result = await this.tdocumentVenta.Get();

            if (!Result.Success)
            {
                ViewBag.Message = Result.Message;
                return View(new List<TDocumentVentaGetModel>());
            }

            return View(Result.Data);
        }


        public async Task<IActionResult> Details(int id)
        {
            var Result = await this.tdocumentVenta.GetById(id);

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
        public async Task<IActionResult> Create(TDocumentDtoAdd documentDto)
        {
            documentDto.UserId = 1;
            documentDto.ChangeDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View(documentDto);
            }

            var result = await this.tdocumentVenta.Save(documentDto);
            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View(documentDto);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var result = await this.tdocumentVenta.GetById(id);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View();
            }

            return View(result.Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TDocumentDtoUpdate dtoUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(dtoUpdate);
            }

            var result = await this.tdocumentVenta.Update(dtoUpdate);

            if (!result.Success)
            {
                return View(dtoUpdate);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
