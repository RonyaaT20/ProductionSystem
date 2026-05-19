using ProductionSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.IRepositories
{
    public interface IParameterValueRepository:IGenericRepository<ParameterValue>
    {
        Task<List<string>> GetUsedValuesAsync(int parameterId, List<string> values);
        Task AddRangeAsync(List<ParameterValue> models);
    }
}
