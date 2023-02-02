namespace DevFreela.Application.ViewModels
{
    public class SkillViewModel
    {
        public int Id { get; private set; }
        public string Description { get; private set; }

        public SkillViewModel(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
