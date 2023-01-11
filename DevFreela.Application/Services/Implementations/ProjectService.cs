using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;
            return projects.Select(project => new ProjectViewModel(project.Id, project.Title, project.CreatedAt)).ToList();
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(project => project.Id == id);
            return new ProjectDetailsViewModel(project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishAt);
        }

        public int Create(NewProjectInputModel projectInputModel)
        {
            var project = new Project(projectInputModel.Title, projectInputModel.Description, projectInputModel.IdClient, projectInputModel.IdFreelancer, projectInputModel.TotalCost);

            _dbContext.Projects.Add(project);

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel commentInputModel)
        {
            var comment = new ProjectComment(commentInputModel.Content, commentInputModel.IdProject, commentInputModel.IdUser);
            _dbContext.ProjectComments.Add(comment);
        }

        public void Update(UpdateProjectInputModel projectInputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(project => project.Id == projectInputModel.Id);

            project.Update(projectInputModel.Title, projectInputModel.Description, projectInputModel.TotalCost);
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(project => project.Id == id);
            project.Cancel();
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(project => project.Id == id);
            project.Start();
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(project => project.Id == id);
            project.Finish();
        }
    }
}
