using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IPermissionService
    {
        bool CheckPermission(int permissionId, int currentUserId);
    }
}
