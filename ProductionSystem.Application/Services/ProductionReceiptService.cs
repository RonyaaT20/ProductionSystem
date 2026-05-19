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
    public class ProductionReceiptService : IProductionReceiptService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductionReceiptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductionReceiptDto>> GetAllAsync()
        {
            var items = await _unitOfWork.ProductionReceipts.GetAllAsync();
            return items.Select(r => new ProductionReceiptDto
            {
                Id = r.Id,
                OrderTitle = r.Order != null ? r.Order.Title : "-",
                ProductTitle = r.Product != null ? r.Product.Title : "-",
                Parameters = r.ProductReceiptParameters != null
                    ? r.ProductReceiptParameters.Select(pp => new ProductParameterItemDto
                    {
                        ParameterId = pp.ProductParameterId,
                        ParameterTitle = pp.ProductParameter != null ? pp.ProductParameter.Title : "-",
                        ValueId = pp.ParameterValueId,
                        ValueTitle = pp.ParameterValue != null ? pp.ParameterValue.Title : "-"
                    }).ToList()
                    : new List<ProductParameterItemDto>(),
                Quantity = r.Quantity,
                Barcode = r.Barcode,
                CreatedAt = r.CreatedAt.ToShamsi()
            }).ToList();
        }

        public async Task<int> CreateAsync(CreateProductionReceiptDto dto)
        {
            try
            {
                var receipt = new ProductionReceipt
                {
                    OrderId = dto.OrderId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    CreatedAt = System.DateTime.Now,
                    Barcode = await _unitOfWork.ProductionReceipts.GenerateUniqueBarcodeAsync(),
                    Personnel = new List<ProductionReceiptPersonnel>(),
                };
                if (dto.PersonnelIds != null)
                    foreach (var pid in dto.PersonnelIds)
                        receipt.Personnel.Add(new ProductionReceiptPersonnel { PersonnelId = pid });

                //receipt.ProductReceiptParameters = dto.ParameterValues != null
                //    ? dto.ParameterValues.Select(p => new ProductionReceiptParameter()
                //    {
                //        ProductParameterId = p.ParameterId,
                //        ParameterValueId = p.ValueId
                //    }).ToList()
                //    : new List<ProductionReceiptParameter>();
                await _unitOfWork.ProductionReceipts.AddAsync(receipt);
                await _unitOfWork.SaveChangesAsync();


                var paramList = dto.ParameterValues != null
                    ? dto.ParameterValues.Select(p => new ProductionReceiptParameter()
                    {
                        ProductionReceiptId = receipt.Id,
                        ProductParameterId = p.ParameterId,
                        ParameterValueId = p.ValueId
                    }).ToList()
                    : new List<ProductionReceiptParameter>();
                await _unitOfWork.ProductionReceipts.AddRangeParameter(paramList);
                await _unitOfWork.SaveChangesAsync();
                return receipt.Id;
            }
            catch { return 0; }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("ProductionReceipt", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.ProductionReceipts.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.ProductionReceipts.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }
    }
}