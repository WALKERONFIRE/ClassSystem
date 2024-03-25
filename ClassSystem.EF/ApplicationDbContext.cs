using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassSystem.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ClassSystem.EF
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)   
        {
            
        }
        public DbSet<Student> Students { get; set; }    
        public DbSet<Course> Courses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
