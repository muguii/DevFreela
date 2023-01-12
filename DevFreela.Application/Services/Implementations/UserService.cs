using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public UserViewModel GetById(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(user => user.Id == id);

            if (user == null)
                return null;

            return new UserViewModel(user.FullName, user.Email);
        }

        public int Create(CreateUserInputModel userInputModel)
        {
            var user = new User(userInputModel.FullName, userInputModel.Email, userInputModel.BirthDate);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }

        public void Login()
        {
            throw new NotImplementedException();
        }
    }
}
