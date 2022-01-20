using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public StartProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _projectRepository.GetByIdAsync(request.Id);
            
            project.Start();

            await _projectRepository.StartAsync(project);

            return Unit.Value;
        }

        // OUTRA MANEIRA
        //public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        //{
        //    await _projectRepository.StartAsync(request.Id);

        //    return Unit.Value;
        //}
    }
}
