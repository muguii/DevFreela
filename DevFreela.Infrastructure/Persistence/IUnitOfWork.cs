using DevFreela.Core.Repositories;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }
}
