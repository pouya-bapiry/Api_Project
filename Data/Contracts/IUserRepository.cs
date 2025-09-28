using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Entities;
using Entities.User;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);

      
    }
}