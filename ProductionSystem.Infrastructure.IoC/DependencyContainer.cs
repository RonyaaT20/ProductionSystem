using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductionSystem.Application.IServices;
using ProductionSystem.Application.Services;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Infrastructure.Data;
using ProductionSystem.Infrastructure.Data.Context;

namespace ProductionSystem.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddProjectServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // دیتابیس
            services.AddDbContext<ProductionSystemDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPersonnelService, PersonnelService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductionReceiptService, ProductionReceiptService>();
            services.AddScoped<IWasteReceiptService, WasteReceiptService>();

            return services;
        }
    }
}