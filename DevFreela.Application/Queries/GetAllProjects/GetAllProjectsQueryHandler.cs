using DevFreela.Application.ViewModels;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, PaginationResult<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            this._projectRepository = projectRepository;
        }

        public async Task<PaginationResult<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var paginationProjects = await _projectRepository.GetAllAsync(request.Query, request.Page);
            var projectsViewModel =  paginationProjects.Data.Select(project => new ProjectViewModel(project.Id, project.Title, project.CreatedAt)).ToList();

            return new PaginationResult<ProjectViewModel>(paginationProjects.Page,
                                                          paginationProjects.TotalPages, 
                                                          paginationProjects.PageSize, 
                                                          paginationProjects.ItemsCount, 
                                                          projectsViewModel);
        }
    }
}
