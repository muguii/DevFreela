using System;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class ProjectViewModel
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ProjectViewModel(int id, string title, DateTime createdAt)
        {
            Id = id;
            Title = title;
            CreatedAt = createdAt;
        }
    }
}
