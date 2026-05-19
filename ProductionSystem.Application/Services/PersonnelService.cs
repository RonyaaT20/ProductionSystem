using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Application.Services
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonnelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PersonnelDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Personnels.GetAllAsync();
            return items.Select(p => new PersonnelDto
            {
                Id = p.Id,
                Title = p.Title,
                Code = p.Code,
                Mobile = p.Mobile
            }).ToList();
        }

        public async Task<PersonnelDto> GetByIdAsync(int id)
        {
            var p = await _unitOfWork.Personnels.GetByIdAsync(id);
            if (p == null) return null;
            return new PersonnelDto
            {
                Id = p.Id,
                Title = p.Title,
                Code = p.Code,
                Mobile = p.Mobile
            };
        }

        public async Task<bool> CreateAsync(CreatePersonnelDto dto)
        {
            try
            {
                var item = new Personnel
                {
                    Title = dto.Title,
                    Code = dto.Code,
                    Mobile = dto.Mobile
                };
                await _unitOfWork.Personnels.AddAsync(item);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> EditAsync(EditPersonnelDto dto)
        {
            try
            {
                var existing = await _unitOfWork.Personnels.GetByIdAsync(dto.Id);
                if (existing == null) return false;
                existing.Title = dto.Title;
                existing.Code = dto.Code;
                existing.Mobile = dto.Mobile;
                _unitOfWork.Personnels.Update(existing);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("Personnel", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.Personnels.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.Personnels.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }
    }
}