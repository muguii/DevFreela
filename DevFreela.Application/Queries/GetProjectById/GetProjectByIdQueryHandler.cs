﻿using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            this._projectRepository = projectRepository;
        }

        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetDetailsByIdAsync(request.Id);

            if (project == null)
                return null;

            return new ProjectDetailsViewModel(project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishAt, project.Client.FullName, project.Freelancer.FullName, project.Comments);
        }
    }
}
