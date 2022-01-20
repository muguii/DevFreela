using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject
{
    internal class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _projectRepository.GetByIdAsync(request.Id);

            project.Update(request.Title, request.Description, request.TocalCost);

            await _projectRepository.SaveChangesAsync(); ;

            return Unit.Value; //Basicamente informa que não possui um retorno
        }
    }
}
