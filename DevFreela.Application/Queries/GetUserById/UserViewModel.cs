using System;

namespace DevFreela.Application.Queries.GetUserById
{
    public class UserViewModel
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }

        public UserViewModel(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }
    }
}
