using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Services;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Infrastructure.Data.Repositories;
using ProductionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class ParameterController : Controller
    {
        private readonly IParameterService _parameterService;
        private readonly IUnitOfWork _unitOfWork;

        public ParameterController(IParameterService parameterService, ProductionSystem.Domain.IRepositories.IUnitOfWork unitOfWork)
        {
            _parameterService = parameterService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _parameterService.GetAllAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _parameterService.GetByIdAsync(id);
            if (item == null) return Json(new { success = false });
            return Json(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJson()
        {
            var items = await _parameterService.GetAllAsync();
            return Json(items.Select(p => new { p.Id, p.Title }));
        }

        [HttpGet]
        public async Task<IActionResult> GetValues(int parameterId)
        {
            var parameter = await _unitOfWork.ProductParameters.GetByIdAsync(parameterId);
            if (parameter == null || parameter.Values == null)
                return Json(new List<object>());
            var values = parameter.Values.Select(v => new { id = v.Id, title = v.Title });
            return Json(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreateParameterDto dto)
        {
            var result = await _parameterService.CreateAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ذخیره سازی" });
        }

        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody] EditParameterDto dto)
        {
            var result = await _parameterService.EditAsync(dto);
            return Json(new { success = result, message = result ? "" : "خطا در ویرایش" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            var result = await _parameterService.DeleteAsync(id);
            return Json(new { success = result.Success, message = result.Message });
        }

        //[HttpPost]
        //public async Task<IActionResult> SaveValues([FromBody] SaveValuesDto dto)
        //{
        //    var result = await _parameterService.SaveValuesAsync(dto.Id, dto.Values);
        //    return Json(new { success = result, message = result ? "" : "خطا در ذخیره مقادیر" });
        //}

        [HttpPost]
        public async Task<IActionResult> CheckValuesUsage([FromBody] CheckValuesUsageDto model)
        {
            var usedValues = await _parameterService
                .GetUsedValuesAsync(model.ParameterId, model.Values);

            return Ok(new { usedValues });
        }

        public class CheckValuesUsageDto
        {
            public int ParameterId { get; set; }
            public List<string> Values { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> SaveValues([FromBody] SaveParameterValuesDto dto)
        {
            var (success, message) = await _parameterService.SaveValuesAsync(dto.Id, dto.Values);
            return Json(new { success, message });
        }
        // ذخیره همه مقادیر (در نهایت)
        public async Task<(bool Success, string Message)> SaveValuesAsync(int parameterId, List<string> values)
        {
            try
            {
                var parameter = await _unitOfWork.ProductParameters.GetByIdAsync(parameterId);
                if (parameter == null)
                    return (false, "پارامتر یافت نشد.");

                // حذف مقادیر قبلی
                await _unitOfWork.RemoveParameterValuesAsync(parameterId);
                await _unitOfWork.SaveChangesAsync();

                // افزودن مقادیر جدید (حذف تکراری)
                if (values != null && values.Any())
                {
                    var uniqueValues = values
                        .Where(v => !string.IsNullOrWhiteSpace(v))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .Select(v => v.Trim())
                        .ToList();

                    parameter.Values = uniqueValues.Select(v => new ParameterValue
                    {
                        Title = v,
                        ProductParameterId = parameterId
                    }).ToList();
                }
                else
                {
                    parameter.Values = new List<ParameterValue>();
                }

                _unitOfWork.ProductParameters.Update(parameter);
                await _unitOfWork.SaveChangesAsync();

                return (true, "مقادیر با موفقیت ذخیره شد.");
            }
            catch (Exception ex)
            {
                // در آینده می‌توانید لاگ کنید
                return (false, "خطا در ذخیره مقادیر پارامتر.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddValue([FromBody] AddValueDto dto)
        {
            try
            {
                var (success, message) = await _parameterService.AddValueAsync(dto.ParameterId, dto.Value);
                return Json(new { success, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteValue(DeleteValueDto dto)
        {
            try
            {
                var (success, message) = await _parameterService.DeleteValueAsync(dto.ParameterId, dto.Value);
                return Json(new { success, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CheckValueUsage(int parameterId, string valueTitle)
        {
            try
            {
                var (isUsed, message) = await _parameterService.IsValueUsedInSystemAsync(parameterId, valueTitle);
                return Json(new { canDelete = !isUsed, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { canDelete = false, message = "خطا در بررسی وابستگی" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RestoreValue([FromBody] RestoreValueDto dto)
        {
            try
            {
                var (success, message) = await _parameterService.RestoreValueAsync(dto.ParameterId, dto.Value);
                return Json(new { success, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedValues(int parameterId)
        {
            var deletedValues = await _parameterService.GetDeletedValuesAsync(parameterId);
            return Json(deletedValues);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateValue([FromBody] UpdateValueDto dto)
        {
            try
            {
                // پیدا کردن پارامتر
                var parameter = await _unitOfWork.ProductParameters.GetByIdAsync(dto.ParameterId);
                if (parameter == null)
                    return Json(new { success = false, message = "پارامتر یافت نشد" });

                // پیدا کردن مقدار مورد نظر
                var valueToEdit = parameter.Values.FirstOrDefault(v => v.Title == dto.OldValue);
                if (valueToEdit == null)
                    return Json(new { success = false, message = "مقدار یافت نشد" });

                // بررسی تکراری نبودن
                if (parameter.Values.Any(v => v.Title == dto.NewValue && v.Id != valueToEdit.Id))
                    return Json(new { success = false, message = "این مقدار تکراری است" });

                // ویرایش مقدار
                valueToEdit.Title = dto.NewValue;
                _unitOfWork.ProductParameters.Update(parameter);
                await _unitOfWork.SaveChangesAsync();

                return Json(new { success = true, message = "مقدار با موفقیت ویرایش شد" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> IsCodeUnique(string code, int id = 0)
        {
            var isUnique = await _parameterService.IsCodeUniqueAsync(code, id);
            return Json(isUnique);
        }

        // کلاس DTO را داخل همان کنترلر یا در فایل جداگانه تعریف کنید
        public class UpdateValueDto
        {
            public int ParameterId { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }
        }
    }
}