using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private const int PAGE_SIZE = 2;

        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<PaginationResult<Project>> GetAllAsync(string query, int page)
        {
            IQueryable<Project> projects = _dbContext.Projects;

            if (!string.IsNullOrEmpty(query))
                projects = projects.Where(p => p.Title.Contains(query) || p.Description.Contains(query));

            return await projects.GetPaged(page, PAGE_SIZE);
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects
                            .Include(project => project.Client)
                            .Include(project => project.Freelancer)
                            .SingleOrDefaultAsync(project => project.Id == id);
        }

        public async Task<Project> GetDetailsByIdAsync(int id)
        {
            return await _dbContext.Projects
                            .Include(project => project.Client)
                            .Include(project => project.Freelancer)
                            .Include(p => p.Comments)
                            .SingleOrDefaultAsync(project => project.Id == id);
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(Project project)
        {
            #region Opção 1

            //var project = await _dbContext.Projects.SingleOrDefaultAsync(project => project.Id == id);

            //project.Start();

            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    await sqlConnection.OpenAsync();
            //    await sqlConnection.ExecuteAsync("UPDATE Project SET Status = @status, StartedAt = @StartedAt WHERE Id = @Id", new { status = project.Status, startedat = project.StartedAt, id });
            //}

            //_dbContext.SaveChanges();

            #endregion

            #region Opção 2

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                await sqlConnection.ExecuteAsync("UPDATE Projects SET Status = @status, StartedAt = @StartedAt WHERE Id = @Id", new { status = project.Status, startedat = project.StartedAt, project.Id });
            }

            #endregion
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddCommentAsync(ProjectComment projectComment)
        {
            await _dbContext.ProjectComments.AddAsync(projectComment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
