using Microsoft.AspNetCore.Mvc;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Services;
using ProductionSystem.Domain.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            return View(users.Where(u => u.Id != 1).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _userService.GetByIdAsync(id);
            if (item == null) return Json(new { success = false });
            return Json(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _userService.GetRolesAsync();
            return Json(roles.Select(r => new { r.Id, r.Title }));
        }


        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreateUserDto dto)
        {
            

            var result = await _userService.CreateAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ذخیره سازی" });
        }
    
        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody] EditUserDto dto)
        {
            if (dto.Id == 1)
                return Json(new { success = false, message = "امکان ویرایش این کاربر وجود ندارد" });

            var result = await _userService.EditAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ویرایش" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var result = await _userService.DeleteAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }

    }
}