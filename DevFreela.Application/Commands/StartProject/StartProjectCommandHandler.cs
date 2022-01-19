using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public StartProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _dbContext.Projects.SingleOrDefaultAsync(project => project.Id == request.Id);

            project.Start();
            
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string sSql = "UPDATE projects SET Status = @status, StartedAt = @StartedAt WHERE ID = @id";
                await sqlConnection.ExecuteAsync(sSql, new { project.Status, project.StartedAt, request.Id });
            }

            return Unit.Value;

            // Com EF CORE
            //dbContext.SaveChanges();
        }
    }
}
