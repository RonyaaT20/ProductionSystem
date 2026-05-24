using Microsoft.AspNetCore.Mvc;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Security;
using ProductionSystem.Application.Services;
using ProductionSystem.Domain.DTOs;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [PermissionChecker(RoleChecker.Role)]

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllAsync();
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _roleService.GetByIdAsync(id);
            if (item == null) return Json(new { success = false });
            return Json(item);
        }
        [PermissionChecker(RoleChecker.CreateRole)]

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreateRoleDto dto)
        {
            var result = await _roleService.CreateAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ذخیره سازی" });
        }
        [PermissionChecker(RoleChecker.EditRole)]

        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody] EditRoleDto dto)
        {
            var result = await _roleService.EditAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ویرایش" });
        }
        [PermissionChecker(RoleChecker.DeleteRole)]

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var result = await _roleService.DeleteAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }
    }
}