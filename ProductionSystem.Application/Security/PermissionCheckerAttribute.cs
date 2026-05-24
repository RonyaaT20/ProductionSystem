using ProductionSystem.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductionSystem.Application.Exceptions;
using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.Utilities;

namespace ProductionSystem.Application.Security
{
    public class PermissionCheckerAttribute : Attribute, IAuthorizationFilter
    {
        private IPermissionService _permissionService;
        private readonly RoleChecker _permissionId;
        private readonly bool _isApi = false;
        public PermissionCheckerAttribute(RoleChecker permissionId, bool isApi = false)
        {
            _permissionId = permissionId;
            _isApi = isApi;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));


            if (_permissionService == null)
                if (!_isApi)
                    context.Result = new RedirectResult("/Permission");
                else
                    throw new ForbiddenException();


            var currentUserId = context.HttpContext.User.GetUserId();

            if (_permissionService != null && !_permissionService.CheckPermission(_permissionId.ToValue(), currentUserId))
                if (!_isApi)
                    context.Result = new RedirectResult("/Permission");
                else
                    throw new ForbiddenException();
        }
    }
}
