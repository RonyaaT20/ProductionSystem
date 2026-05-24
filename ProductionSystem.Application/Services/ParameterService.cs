using ProductionSystem.Application.IServices;
using ProductionSystem.Domain.DTOs;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Application.Services
{
    public class ParameterService : IParameterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParameterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ParameterDto>> GetAllAsync()
        {
            var items = await _unitOfWork.ProductParameters.GetAllAsync();
            return items.Select(p => new ParameterDto
            {
                Id = p.Id,
                Title = p.Title,
                Code = p.Code,
                UnitId = p.UnitId,
                UnitTitle = p.Unit != null ? p.Unit.Title : "-",
                Values = p.Values != null ? p.Values.Select(v => v.Title).ToList() : new List<string>()
            }).ToList();
        }

        public async Task<ParameterDto> GetByIdAsync(int id)
        {
            var p = await _unitOfWork.ProductParameters.GetByIdAsync(id);
            if (p == null) return null;
            return new ParameterDto
            {
                Id = p.Id,
                Title = p.Title,
                Code = p.Code,
                UnitId = p.UnitId,
                UnitTitle = p.Unit != null ? p.Unit.Title : "-",
                Values = p.Values != null ? p.Values.Select(v => v.Title).ToList() : new List<string>()
            };
        }

        public async Task<bool> CreateAsync(CreateParameterDto dto)
        {
            try
            {
                // بررسی تکراری نبودن کد
                var isCodeExists = await _unitOfWork.ProductParameters.IsCodeExistsAsync(dto.Code);
                if (isCodeExists)
                    return false;

                var param = new ProductParameter
                {
                    Title = dto.Title,
                    Code = dto.Code,
                    UnitId = dto.UnitId,
                    Values = dto.Values != null
                        ? dto.Values.Select(v => new ParameterValue { Title = v }).ToList()
                        : new List<ParameterValue>()
                };
                await _unitOfWork.ProductParameters.AddAsync(param);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditAsync(EditParameterDto dto)
        {
            try
            {
                // بررسی تکراری نبودن کد به جز خود آیتم
                var isCodeExists = await _unitOfWork.ProductParameters.IsCodeExistsAsync(dto.Code, dto.Id);
                if (isCodeExists)
                    return false;
                var existing = await _unitOfWork.ProductParameters.GetByIdAsync(dto.Id);
                if (existing == null) return false;
                existing.Title = dto.Title;
                existing.Code = dto.Code;
                existing.UnitId = dto.UnitId;
                _unitOfWork.ProductParameters.Update(existing);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<DeleteResult> DeleteAsync(int id)
        {
            var dependency = await _unitOfWork.CheckDependencyAsync("Parameter", id);
            if (dependency != null)
                return new DeleteResult { Success = false, Message = dependency };

            var item = await _unitOfWork.ProductParameters.GetByIdAsync(id);
            if (item == null) return new DeleteResult { Success = false, Message = "یافت نشد" };
            _unitOfWork.ProductParameters.Delete(item);
            await _unitOfWork.SaveChangesAsync();
            return new DeleteResult { Success = true };
        }

        public async Task<List<string>> GetUsedValuesAsync(int parameterId, List<string> values)
        {
            return await _unitOfWork.ParameterValues
                .GetUsedValuesAsync(parameterId, values);
        }

        public async Task<(bool Success, string Message)> SaveValuesAsync(int parameterId, List<string> newValues)
        {
            try
            {
                var parameter = await _unitOfWork.ProductParameters.GetByIdAsync(parameterId);
                if (parameter == null)
                    return (false, "پارامتر یافت نشد.");

                // آماده‌سازی لیست مقادیر جدید
                var incomingValues = newValues?
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Select(v => v.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList() ?? new List<string>();

                // حذف مقادیر استفاده نشده
                await _unitOfWork.RemoveParameterValuesAsync(parameterId);

                //var newValueModels = new List<ParameterValue>();

                // افزودن مقادیر جدید
                if (incomingValues.Any())
                {
                    var currentTitles = parameter.Values
                        .Select(v => v.Title?.Trim())
                        .Where(t => !string.IsNullOrEmpty(t))
                        .ToHashSet(StringComparer.OrdinalIgnoreCase);

                    var valuesToAdd = incomingValues
                        .Where(title => !currentTitles.Contains(title))
                        .Select(title => new ParameterValue
                        {
                            Title = title,
                            ProductParameterId = parameterId
                        })
                        .ToList();

                    if (valuesToAdd.Any())
                    {
                        foreach (var val in valuesToAdd)
                        {
                            parameter.Values.Add(val); // به جای AddRange
                        }
                    }
                }

                //await _unitOfWork.ParameterValues.AddRangeAsync(newValueModels);
                _unitOfWork.ProductParameters.Update(parameter);
                await _unitOfWork.SaveChangesAsync();

                return (true, "مقادیر با موفقیت به‌روزرسانی شدند.");
            }
            catch (Exception ex)
            {
                return (false, $"خطا در ذخیره مقادیر: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> AddValueAsync(int parameterId, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                    return (false, "مقدار نمی‌تواند خالی باشد");

                var parameter = await _unitOfWork.ProductParameters.GetByIdAsync(parameterId);
                if (parameter == null)
                    return (false, "پارامتر یافت نشد");

                // بررسی وجود مقدار (حتی حذف شده)
                var existingValue = parameter.Values?.FirstOrDefault(v => v.Title == value);

                if (existingValue != null)
                {
                    if (!existingValue.IsDeleted)
                        return (false, "این مقدار قبلاً تعریف شده است");
                    else
                    {
                        // بازیابی مقدار حذف شده
                        existingValue.IsDeleted = false;
                        existingValue.DeletedAt = null;
                        existingValue.DeletedBy = null;
                        await _unitOfWork.SaveChangesAsync();
                        return (true, "مقدار با موفقیت بازیابی شد");
                    }
                }

                // اضافه کردن مقدار جدید
                var newValue = new ParameterValue
                {
                    Title = value,
                    ProductParameterId = parameterId,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now
                };

                if (parameter.Values == null)
                    parameter.Values = new List<ParameterValue>();

                parameter.Values.Add(newValue);
                await _unitOfWork.SaveChangesAsync();

                return (true, "مقدار با موفقیت اضافه شد");
            }
            catch (Exception ex)
            {
                return (false, $"خطا در اضافه کردن مقدار: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteValueAsync(int parameterId, string value)
        {
            try
            {
                var parameter = await _unitOfWork.ProductParameters.GetByIdAsync(parameterId);
                if (parameter == null)
                    return (false, "پارامتر یافت نشد");

                var targetValue = parameter.Values?.FirstOrDefault(v => v.Title == value);
                if (targetValue == null)
                    return (false, "مقدار یافت نشد");

                // بررسی استفاده در سیستم
                var (isUsed, message) = await IsValueUsedInSystemAsync(parameterId, value);
                if (isUsed)
                    return (false, message ?? "این مقدار در سیستم استفاده شده و قابل حذف نیست");

                // Soft Delete
                targetValue.IsDeleted = true;
                targetValue.DeletedAt = DateTime.Now;
                targetValue.DeletedBy = "System"; // می‌توانید از User.Identity.Name استفاده کنید

                _unitOfWork.ProductParameters.Update(parameter);
                await _unitOfWork.SaveChangesAsync();

                return (true, "مقدار با موفقیت حذف شد");
            }
            catch (Exception ex)
            {
                return (false, $"خطا در حذف مقدار: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> RestoreValueAsync(int parameterId, string value)
        {
            try
            {
                var parameter = await _unitOfWork.ProductParameters.GetByIdAsync(parameterId);
                if (parameter == null)
                    return (false, "پارامتر یافت نشد");

                var targetValue = parameter.Values?.FirstOrDefault(v => v.Title == value && v.IsDeleted);
                if (targetValue == null)
                    return (false, "مقدار حذف شده یافت نشد");

                // بازیابی مقدار
                targetValue.IsDeleted = false;
                targetValue.DeletedAt = null;
                targetValue.DeletedBy = null;

                _unitOfWork.ProductParameters.Update(parameter);
                await _unitOfWork.SaveChangesAsync();

                return (true, "مقدار با موفقیت بازیابی شد");
            }
            catch (Exception ex)
            {
                return (false, $"خطا در بازیابی مقدار: {ex.Message}");
            }
        }

        public async Task<List<ParameterValueDto>> GetDeletedValuesAsync(int parameterId)
        {
            try
            {
                var parameter = await _unitOfWork.ProductParameters.GetByIdAsync(parameterId);
                if (parameter == null || parameter.Values == null)
                    return new List<ParameterValueDto>();

                return parameter.Values
                    .Where(v => v.IsDeleted)
                    .Select(v => new ParameterValueDto
                    {
                        Id = v.Id,
                        Title = v.Title,
                        DeletedAt = v.DeletedAt,
                        DeletedBy = v.DeletedBy,
                        CreatedAt = v.CreatedAt
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                // لاگ خطا اگر نیاز دارید
                return new List<ParameterValueDto>();
            }
        }

        public async Task<(bool IsUsed, string Message)> IsValueUsedInSystemAsync(int parameterId, string valueTitle)
        {
            try
            {
                var parameter = await _unitOfWork.ProductParameters.GetByIdAsync(parameterId);
                if (parameter == null)
                    return (true, "پارامتر مورد نظر یافت نشد.");

                // پیدا کردن مقدار مورد نظر
                var valueEntity = parameter.Values?.FirstOrDefault(v =>
                    string.Equals(v.Title, valueTitle, StringComparison.OrdinalIgnoreCase));

                if (valueEntity == null)
                    return (false, null); // مقدار وجود ندارد

                // چک وابستگی‌ها
                var dependency = await _unitOfWork.CheckDependencyAsync("ParameterValue", valueEntity.Id);

                if (!string.IsNullOrEmpty(dependency))
                    return (true, dependency);



                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, "خطا در بررسی وابستگی‌ها");
            }
        }

        public async Task<bool> IsCodeUniqueAsync(string code, int id = 0)
        {
            if (id == 0)
                return !await _unitOfWork.ProductParameters.IsCodeExistsAsync(code);
            else
                return !await _unitOfWork.ProductParameters.IsCodeExistsAsync(code, id);
        }
    }
}