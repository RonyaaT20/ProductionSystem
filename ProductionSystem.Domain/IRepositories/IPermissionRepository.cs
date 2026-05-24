using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.IRepositories
{
    public interface IPermissionRepository
    {
        bool CheckPermission(int permissionId, int currentUserId);
    }
}
