using ProductionSystem.Application.Helpers;
using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Domain.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Application.Services
{
    public class WasteReceiptService : IWasteReceiptService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WasteReceiptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<WasteReceiptDto>> GetAllAsync()
        {
            var items = await _unitOfWork.WasteReceipts.GetAllAsync();
            return items.Select(r => new WasteReceiptDto
            {
                Id = r.Id,
                OrderTitle = r.Order != null ? r.Order.Title : "-",
                ProductTitle = r.Product != null ? r.Product.Title : "-",
                ParameterTitle = r.ProductParameter != null ? r.ProductParameter.Title : "-",
                ParameterValueTitle = r.ParameterValue != null ? r.ParameterValue.Title : "-",
                Quantity = r.Quantity,
                CreatedAt = r.CreatedAt.ToShamsi()
            }).ToList();
        }

        public async Task<bool> CreateAsync(CreateWasteReceiptDto dto)
        {
            try
            {
                var receipt = new WasteReceipt
                {
                    OrderId = dto.OrderId,
                    ProductId = dto.ProductId,
                    ProductParameterId = dto.ProductParameterId,
                    ParameterValueId = dto.ParameterValueId,
                    Quantity = dto.Quantity,
                    CreatedAt = System.DateTime.Now,
                    Personnel = new List<WasteReceiptPersonnel>()
                };
                if (dto.PersonnelIds != null)
                    foreach (var pid in dto.PersonnelIds)
                        receipt.Personnel.Add(new WasteReceiptPersonnel { PersonnelId = pid });
                await _unitOfWork.WasteReceipts.AddAsync(receipt);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("WasteReceipt", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.WasteReceipts.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.WasteReceipts.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }
    }
}