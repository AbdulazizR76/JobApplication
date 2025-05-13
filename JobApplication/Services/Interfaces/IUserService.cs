using JobApplication.Models;
using System.Collections.Generic;

namespace JobApplication.Services.Interfaces
{
    public interface IUserService
    {
        LoginResult ValidateUser(string Email, string password);
        User getUserById(int id);
        IEnumerable<User> GetAll();
        bool UsernameExists(string username);
        void Create(RegisterViewModel model);
        void Update(User user);
        void Delete(int id);
    }
}