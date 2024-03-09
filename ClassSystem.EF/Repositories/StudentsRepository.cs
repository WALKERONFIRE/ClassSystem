using ClassSystem.Core.Interfaces;
using ClassSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSystem.EF.Repositories
{
    public class StudentsRepository : BaseRepository<Student>, IStudentsRepository
    {
        public StudentsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Student> SpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
