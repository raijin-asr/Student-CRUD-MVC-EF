using Microsoft.EntityFrameworkCore;
using Student_CRUD_MVC_EF.Models.Entities;

namespace Student_CRUD_MVC_EF.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
