using JobApplication.Models;
using JobApplication.Services.Interfaces;
using System;
using System.Linq;

namespace JobApplication.Services
{

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICryptoService _cryptoService;
        public UserService(ApplicationDbContext context, ICryptoService cryptoService)
        {
            _context = context;
            _cryptoService = cryptoService;
        }
        public void Create(RegisterViewModel model)
        {
            if (_context.Users.Any(u => u.Email.ToLower() == model.Email))
            {
                throw new Exception("Email already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = model.Email.ToUpper(),
                Email = model.Email.ToLower(),
                PasswordHash = _cryptoService.HashPassword(model.Password),
                Name = model.Name,
                Position = model.Position,
                DepartmentId = model.DepartmentId,
            };

            _context.Users.Add(user);

            var role = _context.Roles.FirstOrDefault(r => r.Name.ToUpper() == "USER");
            if (role == null)
            {
                throw new Exception("Default role 'USER' not found.");
            }

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
            };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

        }

        public void permanentDelete(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        public void SoftDelete(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.DeletionRequestedDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public LoginResult ValidateUser(string Email, string password)
        {
            // lazy loading 
            //var user = _context.Users.FirstOrDefault(u => u.Email == Email);

            // eager loading roles for better for the user 
            var user = _context.Users
                .Include("UserRoles.Role")
                .FirstOrDefault(u => u.Email.ToLower() == Email.ToLower());

            if (user == null)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "No user found with this email."
                };
            }

            if (!user.IsActive)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "This user account is inactive."
                };
            }

            //Validate password (BCrypt and fallback SHA256)
            bool passwordValid = false;

            if (user.PasswordHash.StartsWith("$2a$") || user.PasswordHash.StartsWith("$2b$"))
            {
                passwordValid = _cryptoService.VerifyPassword(password, user.PasswordHash);
            }
            else
            {
                var legacyHash = _cryptoService.HashSha256(password);
                passwordValid = (user.PasswordHash == legacyHash);

                // Auto-upgrade hash
                if (passwordValid)
                {
                    user.PasswordHash = _cryptoService.HashPassword(password);
                    _context.SaveChanges();
                }
            }

            if (!passwordValid)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "Incorrect password."
                };
            }

            // ✅ Step 2: Handle soft deletion
            if (user.IsDeleted)
            {
                if (user.DeletionRequestedDate.HasValue &&
                    DateTime.UtcNow > user.DeletionRequestedDate.Value.AddDays(30))
                {
                    return new LoginResult
                    {
                        Success = false,
                        ErrorMessage = "This account has been permanently deleted."
                    };
                }

                return new LoginResult
                {
                    Success = false,
                    CanReactivate = true,
                    ErrorMessage = "Your account is scheduled for deletion. Do you want to reactivate it?",
                    User = user
                };
            }

            // ❌ inactive user
            if (!user.IsActive)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "This user account is inactive."
                };
            }

            // ✅ All good
            return new LoginResult
            {
                Success = true,
                User = user
            };



        }

        public User GetUserById(string id)
        {
            User user = _context.Users
                .Include("UserRoles.Role")
                .FirstOrDefault(u => u.Id == id);
            return user;
        }

        public void Update(User user)
        {
            var userfromdb = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (userfromdb != null)
            {
                userfromdb.Name = user.Name;
                userfromdb.Position = user.Position;
                userfromdb.DepartmentId = user.DepartmentId;
                userfromdb.IsActive = user.IsActive;
                userfromdb.Email = user.Email.ToLower();
                userfromdb.Username = user.Username.ToUpper();
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public void UpdateProfile(EditProfileViewModel model)
        {
            var u = _context.Users.Find(model.UserId)
                ?? throw new InvalidOperationException("User not found");

            u.Name = model.Name;
            u.Email = model.Email.ToLower();
            u.Position = model.Position;
            u.DepartmentId = model.DepartmentId;
            _context.SaveChanges();
        }


        public bool UsernameExists(string username)
        {
            return _context.Users.Any(u => u.Username == username.ToUpper());
        }

        public ChangePasswordResult ChangePassword(ChangePasswordViewModel model)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == model.UserId);

            bool isValid = _cryptoService.VerifyPassword(model.CurrentPassword, user.PasswordHash);
            if (!isValid)
            {
                return new ChangePasswordResult { Success = false, Message = "Incorrect password." };
            }
            user.PasswordHash = _cryptoService.HashPassword(model.NewPassword);
            _context.SaveChanges();
            return new ChangePasswordResult { Success = true, Message = "Password Changed successfully" };

        }

        public void RestoreAccount(string id)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.IsDeleted == true)
            {
                user.IsDeleted = false;
                user.DeletionRequestedDate = null;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User is not deleted");
            }



        }
    }
}