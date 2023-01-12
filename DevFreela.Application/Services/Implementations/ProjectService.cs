using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;
            return projects.Select(project => new ProjectViewModel(project.Id, project.Title, project.CreatedAt)).ToList();
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects
                            .Include(project => project.Client)
                            .Include(project => project.Freelancer)
                            .SingleOrDefault(project => project.Id == id);

            if (project == null)
                return null;

            return new ProjectDetailsViewModel(project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishAt, project.Client.FullName, project.Freelancer.FullName);
        }

        public int Create(NewProjectInputModel projectInputModel)
        {
            var project = new Project(projectInputModel.Title, projectInputModel.Description, projectInputModel.IdClient, projectInputModel.IdFreelancer, projectInputModel.TotalCost);

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel commentInputModel)
        {
            var comment = new ProjectComment(commentInputModel.Content, commentInputModel.IdProject, commentInputModel.IdUser);
            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();
        }

        public void Update(UpdateProjectInputModel projectInputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(project => project.Id == projectInputModel.Id);

            project.Update(projectInputModel.Title, projectInputModel.Description, projectInputModel.TotalCost);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(project => project.Id == id);
            project.Cancel();
            _dbContext.SaveChanges();
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(project => project.Id == id);
            project.Start();

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Execute("UPDATE Project SET Status = @status, StartedAt = @StartedAt WHERE Id = @Id", new { status = project.Status, startedat = project.StartedAt, id });
            }

            //_dbContext.SaveChanges();
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(project => project.Id == id);
            project.Finish();
            _dbContext.SaveChanges();
        }
    }
}
