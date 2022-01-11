namespace DevFreela.Application.ViewModels
{
    public class ProjectViewModel
    {
        public string Title { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ProjectViewModel(string title, DateTime createdAt)
        {
            Title = title;
            CreatedAt = createdAt;
        }
    }
}
