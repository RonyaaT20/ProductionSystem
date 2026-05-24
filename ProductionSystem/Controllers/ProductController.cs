using Microsoft.AspNetCore.Mvc;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Services;
using ProductionSystem.Domain.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _productService.GetAllAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _productService.GetByIdAsync(id);
            if (item == null) return Json(new { success = false });
            return Json(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJson()
        {
            var items = await _productService.GetAllAsync();
            return Json(items.Select(p => new { p.Id, p.Title }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreateProductDto dto)
        {
            var result = await _productService.CreateAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ذخیره سازی" });
        }

        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody] EditProductDto dto)
        {
            var result = await _productService.EditAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ویرایش" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var result = await _productService.DeleteAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> IsCodeUnique(string code, int id = 0)
        {
            var allItems = await _productService.GetAllAsync();
            bool isUnique = id == 0
                ? !allItems.Any(p => p.Code == code)
                : !allItems.Any(p => p.Code == code && p.Id != id);
            return Json(isUnique);
        }
    }
}