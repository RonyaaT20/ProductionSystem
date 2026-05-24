using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.DTOs
{
    public enum RoleChecker
    {
        [Display(Name = "خانه")] Home = 1,
        [Display(Name = "اطلاعات پایه")] BasicInformation = 2,
        [Display(Name = "تولید")] Production = 3,

        //home
        [Display(Name = "خانه")] MyHome = 4,

        //BasicInformation
        [Display(Name = "کاربر")] User = 5,
        [Display(Name = "سطح دسترسی")] Role = 6,
        [Display(Name = "واحد")] Unit = 7,
        [Display(Name = "پرسنل")] Personnel = 8,
        [Display(Name = "مشتری")] Customer = 9,
        [Display(Name = "پارامتر")] Parameter = 10,
        [Display(Name = "کالا")] Product = 11,

        //Production
        [Display(Name = "سفارش")] Order = 12,
        [Display(Name = "رسید تولید")] ProductionReceipt = 13,
        [Display(Name = "گزارش تولید")] Report = 14,

        //User
        [Display(Name = "افزودن کاربر")] CreateUser = 15,
        [Display(Name = "ویرایش کاربر")] EditUser = 16,
        [Display(Name = "حذف کاربر")] DeleteUser = 17,

        //Role
        [Display(Name = "افزودن سطح دسترسی")] CreateRole = 18,
        [Display(Name = "ویرایش سطح دسترسی")] EditRole = 19,
        [Display(Name = "حذف سطح دسترسی")] DeleteRole = 20,

        //Unit
        [Display(Name = "افزودن واحد")] CreateUnit = 21,
        [Display(Name = "ویرایش واحد")] EditUnit = 22,
        [Display(Name = "حذف واحد")] DeleteUnit = 23,

        //Personnel
        [Display(Name = "افزودن پرسنل")] CreatePersonnel = 24,
        [Display(Name = "ویرایش پرسنل")] EditPersonnel = 25,
        [Display(Name = "حذف پرسنل")] DeletePersonnel = 26,

        //Customer
        [Display(Name = "افزودن مشتری")] CreateCustomer = 27,
        [Display(Name = "ویرایش مشتری")] EditCustomer = 28,
        [Display(Name = "حذف مشتری")] DeleteCustomer = 29,

        //Parameter
        [Display(Name = "افزودن پارامتر")] CreateParameter = 30,
        [Display(Name = "ویرایش پارامتر")] EditParameter = 31,
        [Display(Name = "حذف پارامتر")] DeleteParameter = 32,

        //Product
        [Display(Name = "افزودن کالا")] CreateProduct = 33,
        [Display(Name = "ویرایش کالا")] EditProduct = 34,
        [Display(Name = "حذف کالا")] DeleteProduct = 35,

        //Order
        [Display(Name = "افزودن سفارش")] CreateOrder = 36,
        [Display(Name = "ویرایش سفارش")] EditOrder = 37,
        [Display(Name = "حذف سفارش")] DeleteOrder = 38,

        //ProductionReceipt
        [Display(Name = "افزودن رسید تولید")] CreateProductionReceipt = 39,
        [Display(Name = "ویرایش رسید تولید")] EditProductionReceipt = 40,
        [Display(Name = "حذف رسید تولید")] DeleteProductionReceipt = 41,

        //ParameterValue
        [Display(Name = "مقادیر")] ParameterValue = 42,
        [Display(Name = "افزودن مقادیر")] CreateParameterValue = 43,
        [Display(Name = "ویرایش مقادیر")] EditParameterValue = 44,
        [Display(Name = "حذف مقادیر")] DeleteParameterValue = 45,

    }
}
