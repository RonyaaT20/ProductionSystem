using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Infrastructure.Data.Context;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class PermissionRepository:IPermissionRepository
    {
        private readonly ProductionSystemDbContext _context;

        public PermissionRepository(ProductionSystemDbContext context)
        {
            _context = context;
        }

        public bool CheckPermission(int permissionId, int currentUserId)
        {
            if (currentUserId == 1)
                return true;

            return _context.Users.Any(x =>
                x.Id == currentUserId && x.Role.RolePermissions.Any(y => y.PermissionId == permissionId));

        }
    }
}
