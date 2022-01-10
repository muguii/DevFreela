namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = true;
            CreatedAt = DateTime.Now;

            Skills = new List<UserSkill>();
            OwnedProjects = new List<Projects>();
            FreelanceProjects = new List<Projects>();
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }
        public List<UserSkill> Skills { get; private set; }
        public List<Projects> OwnedProjects { get; private set; }
        public List<Projects> FreelanceProjects { get; private set; }
    }
}
