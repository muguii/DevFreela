using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }

        public DevFreelaDbContext()
        {
            Projects = new List<Project>()
            {
                new Project(".NET Core 1", "Descrição 1", 1, 1, 10000),
                new Project(".NET Core 2", "Descrição 2", 1, 1, 20000),
                new Project(".NET Core 3", "Descrição 3", 1, 1, 30000)
            };

            Users = new List<User>()
            {
                new User("Muriel 1", "muriel1@teste.com", new DateTime(1998, 6, 9)),
                new User("Muriel 2", "muriel2@teste.com", new DateTime(1998, 6, 9)),
                new User("Muriel 3", "muriel3@teste.com", new DateTime(1998, 6, 9))
            };

            Skills = new List<Skill>()
            {
                new Skill("Habilidade 1"),
                new Skill("Habilidade 2"),
                new Skill("Habilidade 3")
            };
        }
    }
}
