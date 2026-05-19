using Microsoft.AspNetCore.Mvc;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Services;
using ProductionSystem.Domain.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class PersonnelController : Controller
    {
        private readonly IPersonnelService _personnelService;

        public PersonnelController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _personnelService.GetAllAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _personnelService.GetByIdAsync(id);
            if (item == null) return Json(new { success = false });
            return Json(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJson()
        {
            var items = await _personnelService.GetAllAsync();
            return Json(items.Select(p => new { p.Id, p.Title }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreatePersonnelDto dto)
        {
            var result = await _personnelService.CreateAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ذخیره سازی" });
        }

        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody] EditPersonnelDto dto)
        {
            var result = await _personnelService.EditAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ویرایش" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var result = await _personnelService.DeleteAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }
    }
}