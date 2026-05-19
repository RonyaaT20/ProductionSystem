using Microsoft.AspNetCore.Mvc;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Services;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.IRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class WasteReceiptController : Controller
    {
        private readonly IWasteReceiptService _receiptService;
        private readonly IUnitOfWork _unitOfWork;

        public WasteReceiptController(IWasteReceiptService receiptService, IUnitOfWork unitOfWork)
        {
            _receiptService = receiptService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _receiptService.GetAllAsync();
            return View(items);
        }

        public async Task<IActionResult> GetProductByOrder(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) return Json(null);

            var product = order.Product;

            var parameters = product?.Parameters?.Select(pp => new
            {
                parameterId = pp.ProductParameterId,
                parameterTitle = pp.ProductParameter != null ? pp.ProductParameter.Title : "",
                values = pp.ProductParameter?.Values?.Select(v => new { id = v.Id, title = v.Title })
            }).ToList();

            return Json(new
            {
                productId = order.ProductId,
                productTitle = product?.Title ?? "",
                parameters = parameters
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreateWasteReceiptDto dto)
        {
            var result = await _receiptService.CreateAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ثبت رسید" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var result = await _receiptService.DeleteAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }
    }
}