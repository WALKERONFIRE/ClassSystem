using ClassSystem.Core.Interfaces;
using ClassSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSystem.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentsRepository Students { get; }
        IBaseRepository<Course> Courses { get; }
        int Complete();

    }
}
