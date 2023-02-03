using DevFreela.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DevFreelaDbContext _context;

        public IProjectRepository Projects { get; }

        public IUserRepository Users { get; }

        public UnitOfWork(DevFreelaDbContext context, IProjectRepository projects, IUserRepository users)
        {
            _context = context;

            Projects = projects;
            Users = users;  
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
