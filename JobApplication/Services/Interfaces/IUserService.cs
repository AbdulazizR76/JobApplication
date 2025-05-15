using JobApplication.Models;
using System.Linq;

namespace JobApplication.Services.Interfaces
{
    public interface IUserService
    {
        LoginResult ValidateUser(string Email, string password);
        User GetUserById(string id);
        IQueryable<User> GetAll();
        bool UsernameExists(string username);
        void Create(RegisterViewModel model);
        void Update(User user);
        void UpdateProfile(EditProfileViewModel model);
        void Delete(string id);

        ChangePasswordResult ChangePassword(ChangePasswordViewModel model);
    }
}