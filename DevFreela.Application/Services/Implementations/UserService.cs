using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserViewModel GetByid(int id)
        {
            User user = dbContext.Users.SingleOrDefault(user => user.Id == id);

            if (user == null)
            {
                return null;
            }

            return new UserViewModel(user.FullName, user.Email);
        }

        public int Create(CreateUserInputModel inputModel)
        {
            User user = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate);
            dbContext.Users.Add(user);

            return user.Id;
        }
    }
}
