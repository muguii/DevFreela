using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }

        public DevFreelaDbContext()
        {
            Projects = new List<Project> 
            {
                new Project("Titulo 1", "Descricao 1", 1, 1, 10000),
                new Project("Titulo 2", "Descricao 2", 2, 2, 20000),
                new Project("Titulo 3", "Descricao 3", 3, 3, 30000)
            };

            Users = new List<User>
            {
                new User("Nome 1", "Email 1", DateTime.Now),
                new User("Nome 2", "Email 2", DateTime.Now),
                new User("Nome 3", "Email 3", DateTime.Now)
            };

            Skills = new List<Skill>
            {
                new Skill("Skill 1"),
                new Skill("Skill 2"),
                new Skill("Skill 3")
            };
        }
    }
}
