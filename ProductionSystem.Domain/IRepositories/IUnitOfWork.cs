using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.IRepositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IUnitRepository Units { get; }
        IProductParameterRepository ProductParameters { get; }
        IParameterValueRepository ParameterValues { get; }
        IProductRepository Products { get; }
        IPersonnelRepository Personnels { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IProductionReceiptRepository ProductionReceipts { get; }
        IWasteReceiptRepository WasteReceipts { get; }

        Task<int> SaveChangesAsync();
        Task RemoveParameterValuesAsync(int parameterId);
        Task<string> CheckDependencyAsync(string entityType, int id);
    }
}
