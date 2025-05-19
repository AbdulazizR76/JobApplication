using JobApplication.Models;
using System.Linq;

namespace JobApplication.Services.Interfaces
{
    public interface IUserService
    {
        LoginResult ValidateUser(string Email, string password);
        ApplicationUser GetUserById(string id);
        IQueryable<ApplicationUser> GetAll();
        bool UsernameExists(string username);
        void Create(RegisterViewModel model);
        void Update(ApplicationUser user);
        void UpdateProfile(EditProfileViewModel model);
        void permanentDelete(string id);

        void SoftDelete(string id);

        void RestoreAccount(string id);

        ChangePasswordResult ChangePassword(ChangePasswordViewModel model);
    }
}