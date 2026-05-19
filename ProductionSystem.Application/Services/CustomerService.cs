using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Customers.GetAllAsync();
            return items.Select(c => new CustomerDto
            {
                Id = c.Id,
                Title = c.Title,
                Code = c.Code,
                Mobile = c.Mobile,
                Province = c.Province,
                City = c.City,
                Address = c.Address
            }).ToList();
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var c = await _unitOfWork.Customers.GetByIdAsync(id);
            if (c == null) return null;
            return new CustomerDto
            {
                Id = c.Id,
                Title = c.Title,
                Code = c.Code,
                Mobile = c.Mobile,
                Province = c.Province,
                City = c.City,
                Address = c.Address
            };
        }

        public async Task<bool> CreateAsync(CreateCustomerDto dto)
        {
            try
            {
                var item = new Customer
                {
                    Title = dto.Title,
                    Code = dto.Code,
                    Mobile = dto.Mobile,
                    Province = dto.Province,
                    City = dto.City,
                    Address = dto.Address
                };
                await _unitOfWork.Customers.AddAsync(item);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> EditAsync(EditCustomerDto dto)
        {
            try
            {
                var existing = await _unitOfWork.Customers.GetByIdAsync(dto.Id);
                if (existing == null) return false;
                existing.Title = dto.Title;
                existing.Code = dto.Code;
                existing.Mobile = dto.Mobile;
                existing.Province = dto.Province;
                existing.City = dto.City;
                existing.Address = dto.Address;
                _unitOfWork.Customers.Update(existing);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("Customer", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.Customers.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.Customers.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }
    }
}