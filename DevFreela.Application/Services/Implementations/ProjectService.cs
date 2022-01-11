using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            return dbContext.Projects.Select(project => new ProjectViewModel(project.Title, project.CreatedAt)).ToList();
        }

        public ProjectDetailsViewModel GetByid(int id)
        {
            Project project = dbContext.Projects.SingleOrDefault(project => project.Id == id);
            return new ProjectDetailsViewModel(project.Id, project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt);
        }

        public int Create(NewProjectInputModel inputModel)
        {
            Project project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);
            dbContext.Projects.Add(project);

            return project.Id;
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            Project project = dbContext.Projects.SingleOrDefault(project => project.Id == id);
            project.Cancel();
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            ProjectComment comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            dbContext.ProjectComments.Add(comment);
        }

        public void Start(int id)
        {
            throw new NotImplementedException();
        }

        public void Finish(int id)
        {
            throw new NotImplementedException();
        }
    }
}
