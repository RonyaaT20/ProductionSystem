using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Infrastructure.Data.Context;
using ProductionSystem.Infrastructure.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionSystem.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductionSystemDbContext _context;S
        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IUnitRepository Units { get; private set; }
        public IProductParameterRepository ProductParameters { get; private set; }
        public IParameterValueRepository ParameterValues { get; private set; }
        public IProductRepository Products { get; private set; }
        public IPersonnelRepository Personnels { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IProductionReceiptRepository ProductionReceipts { get; private set; }
        public IWasteReceiptRepository WasteReceipts { get; private set; }
        public UnitOfWork(ProductionSystemDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Roles = new RoleRepository(context);
            Units = new UnitRepository(context);
            ProductParameters = new ProductParameterRepository(context);
            Products = new ProductRepository(context);
            Personnels = new PersonnelRepository(context);
            Customers = new CustomerRepository(context);
            Orders = new OrderRepository(context);
            ProductionReceipts = new ProductionReceiptRepository(context);
            WasteReceipts = new WasteReceiptRepository(context);
            ParameterValues = new ParameterValueRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task RemoveParameterValuesAsync(int parameterId)
        {
            var values = _context.ParameterValues.Where(v => v.ProductParameterId == parameterId);
            _context.ParameterValues.RemoveRange(values);
            await Task.CompletedTask;
        }

        public async Task<string> CheckDependencyAsync(string entityType, int id)
        {
            switch (entityType)
            {
                case "Unit":
                    var unitInParam = await _context.Parameters
                        .IgnoreQueryFilters()
                        .AnyAsync(p => p.UnitId == id && !p.IsDeleted);
                    if (unitInParam) return "این واحد در پارامترها استفاده شده است";
                    var unitInProduct = await _context.Products
                        .IgnoreQueryFilters()
                        .AnyAsync(p => p.UnitId == id && !p.IsDeleted);
                    if (unitInProduct) return "این واحد در کالاها استفاده شده است";
                    break;

                case "Role":
                    var roleInUser = await _context.Users
                        .IgnoreQueryFilters()
                        .AnyAsync(u => u.RoleId == id && !u.IsDeleted);
                    if (roleInUser) return "این نقش به کاربرانی اختصاص داده شده است";
                    break;

                case "Parameter":
                    var paramInProduct = await _context.ProductParameterItems
                        .IgnoreQueryFilters()
                        .AnyAsync(p => p.ProductParameterId == id && !p.IsDeleted);
                    if (paramInProduct) return "این پارامتر در کالاها استفاده شده است";
                    var paramInReceipt = await _context.ProductionReceipts
                        .IgnoreQueryFilters()
                        .AnyAsync(r => r.ProductReceiptParameters.Any(pi => pi.ProductParameterId == id) && !r.IsDeleted);
                    if (paramInReceipt) return "این پارامتر در رسیدهای تولید استفاده شده است";
                    break;

                case "Product":
                    var productInOrder = await _context.Orders
                        .IgnoreQueryFilters()
                        .AnyAsync(o => o.ProductId == id && !o.IsDeleted);
                    if (productInOrder) return "این کالا در سفارشات استفاده شده است";
                    var productInReceipt = await _context.ProductionReceipts
                        .IgnoreQueryFilters()
                        .AnyAsync(r => r.ProductId == id && !r.IsDeleted);
                    if (productInReceipt) return "این کالا در رسیدهای تولید استفاده شده است";
                    break;

                case "Customer":
                    var customerInOrder = await _context.Orders
                        .IgnoreQueryFilters()
                        .AnyAsync(o => o.CustomerId == id && !o.IsDeleted);
                    if (customerInOrder) return "این مشتری در سفارشات استفاده شده است";
                    break;

                case "Order":
                    var orderInReceipt = await _context.ProductionReceipts
                        .IgnoreQueryFilters()
                        .AnyAsync(r => r.OrderId == id && !r.IsDeleted);
                    if (orderInReceipt) return "این سفارش در رسیدهای تولید استفاده شده است";
                    break;

                case "Personnel":
                    var personnelInReceipt = await _context.ProductionReceiptPersonnels
                        .IgnoreQueryFilters()
                        .AnyAsync(p => p.PersonnelId == id);
                    if (personnelInReceipt) return "این پرسنل در رسیدهای تولید استفاده شده است";
                    break;
                case "ParameterValue":
                    var parameterValueResult = 
                        await _context.ProductionReceiptParameters.IgnoreQueryFilters().AnyAsync(p => p.ParameterValueId == id)
                        || await _context.ProductParameterItems.IgnoreQueryFilters().AnyAsync(p => p.ParameterValueId == id);
                    if (parameterValueResult) return "مقدار مورد نظر در سیستم استفاده شده است!";
                    break;
            }
            return null;
        }
    }
}
