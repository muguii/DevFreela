using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext dbContext;
        private readonly string connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            connectionString = configuration.GetConnectionString("DevFreelaCs");

        }

        public List<ProjectViewModel> GetAll(string query)
        {
            return dbContext.Projects.Select(project => new ProjectViewModel(project.Id, project.Title, project.CreatedAt)).ToList();
        }

        public ProjectDetailsViewModel GetByid(int id)
        {
            Project project = dbContext.Projects.Include(project => project.Client)
                                                .Include(project => project.Freelancer)
                                                .SingleOrDefault(project => project.Id == id);

            if (project == null)
            {
                return null;
            }

            return new ProjectDetailsViewModel(project.Id, project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt, project.Client.FullName, project.Freelancer.FullName);
        }

        public int Create(CreateProjectInputModel inputModel)
        {
            Project project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);

            dbContext.Projects.Add(project);
            dbContext.SaveChanges();

            return project.Id;
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            Project project = dbContext.Projects.SingleOrDefault(project => project.Id == inputModel.Id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TocalCost);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Project project = dbContext.Projects.SingleOrDefault(project => project.Id == id);

            project.Cancel();
            dbContext.SaveChanges();
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            ProjectComment comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);

            dbContext.ProjectComments.Add(comment);
            dbContext.SaveChanges();
        }

        public void Start(int id)
        {
            Project project = dbContext.Projects.SingleOrDefault(project => project.Id == id);

            project.Start();
            //dbContext.SaveChanges();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string sSql = "UPDATE projects SET Status = @status, StartedAt = @StartedAt WHERE ID = @id";
                sqlConnection.Execute(sSql, new { project.Status, project.StartedAt, id });
            }
        }

        public void Finish(int id)
        {
            Project project = dbContext.Projects.SingleOrDefault(project => project.Id == id);

            project.Finish();
            dbContext.SaveChanges();
        }
    }
}
