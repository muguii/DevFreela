using Dapper;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public StartProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(project => project.Id == request.Id);
            project.Start();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                await sqlConnection.ExecuteAsync("UPDATE Project SET Status = @status, StartedAt = @StartedAt WHERE Id = @Id", new { status = project.Status, startedat = project.StartedAt, request.Id });
            }

            //_dbContext.SaveChanges();
            return Unit.Value;
        }
    }
}
