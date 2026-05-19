using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductionSystem.Application.Helpers;

namespace ProductionSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Username = u.Username,
                Mobile = u.Mobile,
                RoleId = u.RoleId,
                RoleTitle = u.Role != null ? u.Role.Title : "-",
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt.ToShamsi()
            }).ToList();
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var u = await _unitOfWork.Users.GetByIdAsync(id);
            if (u == null || u.IsDeleted) return null;
            return new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Username = u.Username,
                Mobile = u.Mobile,
                RoleId = u.RoleId,
                RoleTitle = u.Role != null ? u.Role.Title : "-",
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt.ToString("yyyy/MM/dd")
            };
        }

        public async Task<bool> CreateAsync(CreateUserDto dto)
        {
            try
            {
                var user = new User
                {
                    FullName = dto.FullName,
                    Username = dto.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Mobile = dto.Mobile,
                    RoleId = dto.RoleId,
                    IsActive = dto.IsActive,
                    CreatedAt = System.DateTime.Now
                };
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> EditAsync(EditUserDto dto)
        {
            try
            {
                var existing = await _unitOfWork.Users.GetByIdAsync(dto.Id);
                if (existing == null) return false;
                existing.FullName = dto.FullName;
                existing.Username = dto.Username;
                existing.Mobile = dto.Mobile;
                existing.RoleId = dto.RoleId;
                existing.IsActive = dto.IsActive;
                if (!string.IsNullOrEmpty(dto.Password))
                    existing.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                _unitOfWork.Users.Update(existing);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("User", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.Users.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.Users.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }

        public async Task<List<RoleDto>> GetRolesAsync()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            return roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Title = r.Title
            }).ToList();
        }
    }
}