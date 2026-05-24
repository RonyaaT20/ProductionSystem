using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace ProductionSystem.Filters
{
    public class AuthFilter : IActionFilter
    {
        private static readonly Dictionary<string, string> ControllerPermissions = new Dictionary<string, string>
        {
            { "User", "User" },
            { "Role", "Role" },
            { "Unit", "Unit" },
            { "Personnel", "Personnel" },
            { "Customer", "Customer" },
            { "Parameter", "Parameter" },
            { "Product", "Product" },
            { "Order", "Order" },
            { "ProductionReceipt", "ProductionReceipt" },
            { "WasteReceipt", "WasteReceipt" },
            { "Report", "Report" }
        };

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var session = context.HttpContext.Session;
            //var userId = session.GetString("UserId");
            //var controller = context.RouteData.Values["controller"].ToString();
            //var action = context.RouteData.Values["action"].ToString();

            //// صفحه لاگین نیاز به چک ندارد
            //if (controller == "Account")
            //    return;

            //// اگر لاگین نکرده
            //if (string.IsNullOrEmpty(userId))
            //{
            //    context.Result = new RedirectToActionResult("Login", "Account", null);
            //    return;
            //}
            //// Home همیشه دسترسی داره
            //if (controller == "Home")
            //    return;

            //// چک سطح دسترسی
            //if (ControllerPermissions.ContainsKey(controller))
            //{
            //    var requiredPermission = ControllerPermissions[controller];
            //    var userPermissions = session.GetString("Permissions") ?? "";

            //    if (!userPermissions.Contains(requiredPermission))
            //    {
            //        context.Result = new ContentResult
            //        {
            //            Content = "<div style='padding:40px;text-align:center;font-family:Tahoma'><h3>دسترسی ندارید</h3><p>شما به این بخش دسترسی ندارید.</p></div>",
            //            ContentType = "text/html",
            //            StatusCode = 403
            //        };
            //        return;
            //    }
            //}
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}