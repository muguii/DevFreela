using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectDetailsById
{
    public class GetProjectDetailsByIdQueryHandler : IRequestHandler<GetProjectDetailsByIdQuery, ProjectDetailsViewModel>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectDetailsByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            Project project = await _projectRepository.GetDetailsByIdAsync(request.Id);

            if (project == null)
            {
                return null;
            }

            ProjectDetailsViewModel projectViewModel = new ProjectDetailsViewModel(project.Id, project.Title, project.Description, 
                                                                                   project.TotalCost, project.StartedAt, project.FinishedAt, 
                                                                                   project.Client.FullName, project.Freelancer.FullName);

            return projectViewModel;
        }
    }
}
