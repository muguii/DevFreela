namespace DevFreela.Core.Entities
{
    public class ProjectComment : BaseEntity
    {
        public string Content { get; private set; }
        public int IdProject { get; private set; }
        public Project Project { get; private set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProjectComment(string content, int idProject, int idUser)
        {
            Content = content;
            IdProject = idProject;
            IdUser = idUser;
            CreatedAt = DateTime.Now;
        }
    }
}
