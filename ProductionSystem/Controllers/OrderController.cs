using Microsoft.AspNetCore.Mvc;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Security;
using ProductionSystem.Application.Services;
using ProductionSystem.Domain.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [PermissionChecker(RoleChecker.Order)]
        public async Task<IActionResult> Index()
        {
            var items = await _orderService.GetAllAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _orderService.GetByIdAsync(id);
            if (item == null) return Json(new { success = false });
            return Json(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJson()
        {
            var items = await _orderService.GetAllAsync();
            return Json(items.Select(o => new { o.Id, o.Title }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreateOrderDto dto)
        {
            var result = await _orderService.CreateAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ذخیره سازی" });
        }
        [PermissionChecker(RoleChecker.EditOrder)]
        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody] EditOrderDto dto)
        {
            var result = await _orderService.EditAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ویرایش" });
        }
        [PermissionChecker(RoleChecker.DeleteOrder)]
        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var result = await _orderService.DeleteAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> IsCodeUnique(string code, int id = 0)
        {
            var allItems = await _orderService.GetAllAsync();
            bool isUnique = id == 0
                ? !allItems.Any(o => o.Code == code)
                : !allItems.Any(o => o.Code == code && o.Id != id);
            return Json(isUnique);
        }
    }
}