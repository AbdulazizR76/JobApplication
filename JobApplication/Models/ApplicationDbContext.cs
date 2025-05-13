using System.Data.Entity;

namespace JobApplication.Models
{
    public class ApplicationDbContext
    {

        public DbSet<Department> Departments { get; set; }



    }
}