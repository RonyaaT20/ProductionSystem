using ProductionSystem.Application.Helpers;
using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Application.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UnitDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Units.GetAllAsync();
            return items.Select(u => new UnitDto
            {
                Id = u.Id,
                Title = u.Title,
                UnitKind = u.UnitKind,
                DecimalPlaces = u.DecimalPlaces,
                CreatedBy = u.CreatedBy,
                CreatedAt = u.CreatedAt.ToShamsi()
            }).ToList();
        }

        public async Task<UnitDto> GetByIdAsync(int id)
        {
            var u = await _unitOfWork.Units.GetByIdAsync(id);
            if (u == null) return null;
            return new UnitDto
            {
                Id = u.Id,
                Title = u.Title,
                UnitKind = u.UnitKind,
                DecimalPlaces = u.DecimalPlaces,
                CreatedBy = u.CreatedBy,
                CreatedAt = u.CreatedAt.ToShamsi()
            };
        }

        public async Task<bool> CreateAsync(CreateUnitDto dto, string createdBy)
        {
            try
            {
                var unit = new Unit
                {
                    Title = dto.Title,
                    UnitKind = dto.UnitKind,
                    DecimalPlaces = dto.DecimalPlaces,
                    CreatedBy = createdBy,
                    CreatedAt = System.DateTime.Now
                };
                await _unitOfWork.Units.AddAsync(unit);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> EditAsync(EditUnitDto dto)
        {
            try
            {
                var existing = await _unitOfWork.Units.GetByIdAsync(dto.Id);
                if (existing == null) return false;
                existing.Title = dto.Title;
                existing.UnitKind = dto.UnitKind;
                existing.DecimalPlaces = dto.DecimalPlaces;
                _unitOfWork.Units.Update(existing);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("Unit", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.Units.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.Units.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }

        public async Task<string> DeleteTestAsync(int id)
        {
            try
            {
                var item = await _unitOfWork.Units.GetByIdAsync(id);
                if (item == null) return "item is null";
                _unitOfWork.Units.Delete(item);
                await _unitOfWork.SaveChangesAsync();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
        }
    }
}