using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetDetailsByIdAsync(int id)
        {
            return await _dbContext.Projects.Include(project => project.Client)
                                            .Include(project => project.Freelancer)
                                            .SingleOrDefaultAsync(project => project.Id == id);
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects.SingleOrDefaultAsync(project => project.Id == id);
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string sSql = "UPDATE projects SET Status = @status, StartedAt = @StartedAt WHERE ID = @id";
                await sqlConnection.ExecuteAsync(sSql, new { project.Status, project.StartedAt, project.Id });
            }
        }

        // OUTRA MANEIRA
        //public async Task StartAsync(int id)
        //{
        //    Project project = await _dbContext.Projects.SingleOrDefaultAsync(project => project.Id == id);

        //    project.Start();

        //    using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        //    {
        //        sqlConnection.Open();

        //        string sSql = "UPDATE projects SET Status = @status, StartedAt = @StartedAt WHERE ID = @id";
        //        await sqlConnection.ExecuteAsync(sSql, new { project.Status, project.StartedAt, id });
        //    }
        //}

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddCommentAsync(ProjectComment comment)
        {
            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
