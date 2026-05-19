using Microsoft.AspNetCore.Mvc;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Services;
using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _customerService.GetAllAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _customerService.GetByIdAsync(id);
            if (item == null) return Json(new { success = false });
            return Json(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJson()
        {
            var items = await _customerService.GetAllAsync();
            return Json(items.Select(c => new { c.Id, c.Title }));
        }

        public IActionResult GetCities(string province)
        {
            var cities = new Dictionary<string, List<string>>
            {
                { "تهران", new List<string> { "تهران", "شهریار", "ری", "دماوند" } },
                { "اصفهان", new List<string> { "اصفهان", "کاشان", "نجف آباد", "خمینی شهر" } },
                { "خراسان رضوی", new List<string> { "مشهد", "نیشابور", "سبزوار", "تربت حیدریه" } },
                { "فارس", new List<string> { "شیراز", "مرودشت", "کازرون", "آباده" } },
                { "آذربایجان شرقی", new List<string> { "تبریز", "مراغه", "مرند", "اهر" } },
            };
            if (cities.ContainsKey(province))
                return Json(cities[province]);
            return Json(new List<string>());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreateCustomerDto dto)
        {
            var result = await _customerService.CreateAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ذخیره سازی" });
        }

        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody] EditCustomerDto dto)
        {
            var result = await _customerService.EditAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ویرایش" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var result = await _customerService.DeleteAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }
    }
}