using System.ComponentModel.DataAnnotations;

namespace ProductionSystem.Application.Api
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "درخواست با موفقیت انجام شده است")]
        Success = 200,

        [Display(Name = "درخواست به دلیل نحو نادرست توسط سرور قابل درک نیست")]
        BadRequest = 400,

        [Display(Name = "درخواست به اطلاعات احراز هویت کاربر نیاز دارد")]
        Unauthorized = 401,

        [Display(Name = "درخواست غیرمجاز مشتری حق دسترسی به محتوا را ندارد")]
        Forbidden = 403,

        [Display(Name = "داده یافت نشد")]
        NotFound = 404,

        [Display(Name = "درخواست به دلیل تضاد با وضعیت فعلی منبع تکمیل نشد")]
        Conflict = 409,

        [Display(Name = "سرور با یک وضعیت غیرمنتظره مواجه شد که مانع از انجام درخواست شد")]
        InternalServerError = 500,

        [Display(Name = "سرور نوع محتوا و نحو موجودیت درخواست را درک می کند، اما همچنان سرور به دلایلی قادر به پردازش درخواست نیست")]
        UnProcessableEntityError = 422,

    }
}