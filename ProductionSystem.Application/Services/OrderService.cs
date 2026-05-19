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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Orders.GetAllAsync();
            return items.Select(o => new OrderDto
            {
                Id = o.Id,
                Title = o.Title,
                Code = o.Code,
                CustomerId = o.CustomerId,
                CustomerTitle = o.Customer != null ? o.Customer.Title : "-",
                ProductId = o.ProductId,
                ProductTitle = o.Product != null ? o.Product.Title : "-",
                DeliveryDate = o.DeliveryDate.ToShamsi()
            }).ToList();
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var o = await _unitOfWork.Orders.GetByIdAsync(id);
            if (o == null) return null;
            return new OrderDto
            {
                Id = o.Id,
                Title = o.Title,
                Code = o.Code,
                CustomerId = o.CustomerId,
                CustomerTitle = o.Customer != null ? o.Customer.Title : "-",
                ProductId = o.ProductId,
                ProductTitle = o.Product != null ? o.Product.Title : "-",
                DeliveryDate = o.DeliveryDate.ToShamsi()
            };
        }

        public async Task<bool> CreateAsync(CreateOrderDto dto)
        {
            try
            {
                var item = new Order
                {
                    Title = dto.Title,
                    Code = dto.Code,
                    CustomerId = dto.CustomerId,
                    ProductId = dto.ProductId,
                    DeliveryDate = dto.DeliveryDate.ToMiladi()
                };
                await _unitOfWork.Orders.AddAsync(item);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> EditAsync(EditOrderDto dto)
        {
            try
            {
                var existing = await _unitOfWork.Orders.GetByIdAsync(dto.Id);
                if (existing == null) return false;
                existing.Title = dto.Title;
                existing.Code = dto.Code;
                existing.CustomerId = dto.CustomerId;
                existing.ProductId = dto.ProductId;
                existing.DeliveryDate = dto.DeliveryDate.ToMiladi();
                _unitOfWork.Orders.Update(existing);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("Order", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.Orders.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.Orders.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }
    }
}