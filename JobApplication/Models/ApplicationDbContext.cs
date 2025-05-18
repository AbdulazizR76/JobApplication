using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace JobApplication.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Department> Departments { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AppFile> AppFiles { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobApplicationRequest> JobApplications { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserRole>()
        //        .HasKey(ur => new { ur.UserId, ur.RoleId });

        //    base.OnModelCreating(modelBuilder);
        //}


    }


}