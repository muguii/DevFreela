using Dapper;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext dbContext;
        private readonly string connectionString;

        public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public List<SkillViewModel> GetAll()
        {
            //return dbContext.Projects.Select(skill => new SkillViewModel(skill.Id, skill.Description)).ToList();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string sSql = "SELECT Id, Description FROM SKILLS";
                return sqlConnection.Query<SkillViewModel>(sSql).ToList();
            }
        }
    }
}
