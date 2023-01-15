using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetProjectByIdQueryHandler(DevFreelaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects
                                .Include(project => project.Client)
                                .Include(project => project.Freelancer)
                                .SingleOrDefaultAsync(project => project.Id == request.Id);

            if (project == null)
                return null;

            return new ProjectDetailsViewModel(project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishAt, project.Client.FullName, project.Freelancer.FullName);
        }
    }
}
