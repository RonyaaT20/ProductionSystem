using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Security;
using ProductionSystem.Domain.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _unitService;

        private readonly ProductionSystem.Domain.IRepositories.IUnitOfWork _unitOfWork;

        public UnitController(IUnitService unitService, ProductionSystem.Domain.IRepositories.IUnitOfWork unitOfWork)
        {
            _unitService = unitService;
            _unitOfWork = unitOfWork;
        }
        [PermissionChecker(RoleChecker.Unit)]

        public async Task<IActionResult> Index()
        {
            var units = await _unitService.GetAllAsync();
            return View(units);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _unitService.GetByIdAsync(id);
            if (item == null) return Json(new { success = false });
            return Json(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJson()
        {
            var items = await _unitService.GetAllAsync();
            return Json(items.Select(u => new { u.Id, u.Title }));
        }
        [PermissionChecker(RoleChecker.CreateUnit)]

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreateUnitDto dto)
        {
            var createdBy = HttpContext.Session.GetString("FullName");
            var result = await _unitService.CreateAsync(dto, createdBy);
            return Json(new { success = result, message = result ? "" : "خطا در ذخیره سازی" });
        }
        [PermissionChecker(RoleChecker.EditUnit)]

        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody] EditUnitDto dto)
        {
            var result = await _unitService.EditAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ویرایش" });
        }
        [PermissionChecker(RoleChecker.DeleteUnit)]

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var result = await _unitService.DeleteAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }


    }
}