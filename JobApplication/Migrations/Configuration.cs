namespace JobApplication.Migrations
{
    using JobApplication.Models;
    using JobApplication.Utilty;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JobApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JobApplication.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //seed departments if it's empty
            if (!context.Departments.Any())
            {
                context.Departments.AddOrUpdate(
                d => d.Code,
                    new Department { Code = SD.DepartmentCode_HR, Name = "Human Resources" },
                    new Department { Code = SD.DepartmentCode_IT, Name = "Information Technology" },
                    new Department { Code = SD.DepartmentCode_Finance, Name = "Finance" },
                    new Department { Code = SD.DepartmentCode_Marketing, Name = "Marketing" }

                );
            }
            // seed roles if it's empty 
            if (!context.Roles.Any())
            {
                context.Roles.AddOrUpdate(

               r => r.Name,
                   new Role { Name = "Admin" },
                   new Role { Name = "User" }
               );
            }

        }
    }
}
