using Dapper;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
    {
        private readonly string _connectionString;

        public GetAllSkillsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string sSql = "SELECT Id, Description FROM SKILLS";
                IEnumerable<SkillViewModel> skills = await sqlConnection.QueryAsync<SkillViewModel>(sSql);

                return skills.ToList();
            }

            // Com EF CORE
            // return dbContext.Projects.Select(skill => new SkillViewModel(skill.Id, skill.Description)).ToList();
        }
    }
}
