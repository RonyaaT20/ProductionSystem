using System.Threading.Tasks;
using ProductionSystem.Domain.Entities;

namespace ProductionSystem.Domain.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
