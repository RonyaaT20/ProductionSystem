using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Products.GetAllAsync();
            return items.Select(p => new ProductDto
            {
                Id = p.Id,
                Title = p.Title,
                Code = p.Code,
                UnitId = p.UnitId,
                UnitTitle = p.Unit != null ? p.Unit.Title : "-",
                Parameters = p.Parameters != null
                    ? p.Parameters.Select(pp => new ProductParameterItemDto
                    {
                        ParameterId = pp.ProductParameterId,
                        ParameterTitle = pp.ProductParameter != null ? pp.ProductParameter.Title : "-",
                        ValueId = pp.ParameterValueId,
                        ValueTitle = pp.ParameterValue != null ? pp.ParameterValue.Title : "-"
                    }).ToList()
                    : new List<ProductParameterItemDto>()
            }).ToList();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var p = await _unitOfWork.Products.GetByIdAsync(id);
            if (p == null) return null;
            return new ProductDto
            {
                Id = p.Id,
                Title = p.Title,
                Code = p.Code,
                UnitId = p.UnitId,
                UnitTitle = p.Unit != null ? p.Unit.Title : "-",
                Parameters = p.Parameters != null
                    ? p.Parameters.Select(pp => new ProductParameterItemDto
                    {
                        ParameterId = pp.ProductParameterId,
                        ParameterTitle = pp.ProductParameter != null ? pp.ProductParameter.Title : "-",
                        ValueId = pp.ParameterValueId,
                        ValueTitle = pp.ParameterValue != null ? pp.ParameterValue.Title : "-"
                    }).ToList()
                    : new List<ProductParameterItemDto>()
            };
        }

        public async Task<bool> CreateAsync(CreateProductDto dto)
        {
            try
            {
                // بررسی تکراری نبودن کد
                var isCodeUnique = await IsCodeUniqueAsync(dto.Code);
                if (!isCodeUnique)
                    return false;
                var product = new Product
                {
                    Title = dto.Title,
                    Code = dto.Code,
                    UnitId = dto.UnitId,
                    Parameters = dto.Parameters != null
                        ? dto.Parameters.Select(p => new ProductParameterItem
                        {
                            ProductParameterId = p.ParameterId,
                            ParameterValueId = p.ValueId
                        }).ToList()
                        : new List<ProductParameterItem>()
                };
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> EditAsync(EditProductDto dto)
        {
            try
            {
                // بررسی تکراری نبودن کد به جز خود آیتم
                var isCodeUnique = await IsCodeUniqueAsync(dto.Code, dto.Id);
                if (!isCodeUnique)
                    return false;
                var existing = await _unitOfWork.Products.GetByIdAsync(dto.Id);
                if (existing == null) return false;
                existing.Title = dto.Title;
                existing.Code = dto.Code;
                existing.UnitId = dto.UnitId;
                if (existing.Parameters != null) existing.Parameters.Clear();
                else existing.Parameters = new List<ProductParameterItem>();
                if (dto.Parameters != null)
                    foreach (var p in dto.Parameters)
                        existing.Parameters.Add(new ProductParameterItem
                        {
                            ProductParameterId = p.ParameterId,
                            ParameterValueId = p.ValueId
                        });
                _unitOfWork.Products.Update(existing);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("Product", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.Products.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.Products.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }

        public async Task<string> DeleteTestAsync(int id)
        {
            try
            {
                var item = await _unitOfWork.Products.GetByIdAsync(id);
                if (item == null) return "item is null";
                _unitOfWork.Products.Delete(item);
                await _unitOfWork.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
        }

        public async Task<bool> IsCodeUniqueAsync(string code, int id = 0)
        {
            var allProducts = await GetAllAsync();

            if (id == 0)
                return !allProducts.Any(p => p.Code == code);
            else
                return !allProducts.Any(p => p.Code == code && p.Id != id);
        }
    }
}