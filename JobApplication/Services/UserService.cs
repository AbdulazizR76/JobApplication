using JobApplication.Models;
using JobApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JobApplication.Services
{

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Create(RegisterViewModel model)
        {
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                throw new Exception("Email already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = model.Email.ToUpper(),
                Email = model.Email,
                PasswordHash = HashPassword(model.Password),
                Name = model.Name,
                Position = model.Position,
                DepartmentId = model.DepartmentId,
            };

            _context.Users.Add(user);



            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = 2 // Assuming 2 is the role ID for "User"
            };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public LoginResult ValidateUser(string Email, string password)
        {
            var user = _context.Users.FirstOrDefault(u =>
        u.Email == Email);

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

            var hashedPassword = HashPassword(password);
            if (user.PasswordHash != hashedPassword)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "Incorrect password."
                };
            }

            return new LoginResult
            {
                Success = true,
                User = user
            };


        }

        public User getUserById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public bool UsernameExists(string username)
        {
            throw new NotImplementedException();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}