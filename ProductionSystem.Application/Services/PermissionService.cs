using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public bool CheckPermission(int permissionId, int currentUserId)
        {
            return _permissionRepository.CheckPermission(permissionId, currentUserId);
        }
    }
}
