using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{
    internal class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;

        public UpdateProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = _dbContext.Projects.SingleOrDefault(project => project.Id == request.Id);

            project.Update(request.Title, request.Description, request.TocalCost);
            await _dbContext.SaveChangesAsync();

            return Unit.Value; //Basicamente informa que não possui um retorno
        }
    }
}
