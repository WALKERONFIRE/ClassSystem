using ClassSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassSystem.Core.Interfaces
{
    public interface IStudentsRepository: IBaseRepository<Student>
    {
        Task<Student> SpecialMethod();
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetJwtToken(TokenRequestModel model);
        Task<string> AddToRoleAsync(AddToRoleModel model);
    }
}
