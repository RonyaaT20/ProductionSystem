using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Application.Helpers;
using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            return roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Title = r.Title,
                CreatedAt = r.CreatedAt.ToShamsi(),
                Permissions = r.RolePermissions != null
                    ? r.RolePermissions.Select(p => p.PermissionKey).ToList()
                    : new List<string>()
            }).ToList();
        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var r = await _unitOfWork.Roles.GetByIdAsync(id);
            if (r == null) return null;
            return new RoleDto
            {
                Id = r.Id,
                Title = r.Title,
                CreatedAt = r.CreatedAt.ToString("yyyy/MM/dd"),
                Permissions = r.RolePermissions != null
                    ? r.RolePermissions.Select(p => p.PermissionKey).ToList()
                    : new List<string>()
            };
        }

        public async Task<bool> CreateAsync(CreateRoleDto dto)
        {
            try
            {
                var role = new Role
                {
                    Title = dto.Title,
                    CreatedAt = System.DateTime.Now,
                    RolePermissions = dto.Permissions != null
                        ? dto.Permissions.Select(p => new RolePermission { PermissionKey = p }).ToList()
                        : new List<RolePermission>()
                };
                await _unitOfWork.Roles.AddAsync(role);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> EditAsync(EditRoleDto dto)
        {
            try
            {
                var existing = await _unitOfWork.Roles.GetByIdAsync(dto.Id);
                if (existing == null) return false;
                existing.Title = dto.Title;
                if (existing.RolePermissions != null) existing.RolePermissions.Clear();
                else existing.RolePermissions = new List<RolePermission>();
                if (dto.Permissions != null)
                    foreach (var p in dto.Permissions)
                        existing.RolePermissions.Add(new RolePermission { PermissionKey = p });
                _unitOfWork.Roles.Update(existing);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("Role", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.Roles.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.Roles.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }
    }
}