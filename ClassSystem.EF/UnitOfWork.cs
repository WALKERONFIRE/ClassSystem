using ClassSystem.Core;
using ClassSystem.Core.Interfaces;
using ClassSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSystem.EF
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IStudentsRepository students, IBaseRepository<Course> courses)
        {
            _context = context;
            Students = students;
            Courses = courses;
        }
        public IStudentsRepository Students { get; private set; }
        public IBaseRepository<Course> Courses { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
