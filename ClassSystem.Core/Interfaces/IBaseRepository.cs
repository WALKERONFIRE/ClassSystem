using ClassSystem.Core.Consts;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassSystem.Core.Interfaces
{
    public interface IBaseRepository<T>where T: class
    {
        T GetById(int id); // sync
        Task<T> GetByIdAsync(int id); //async
        Task <IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T , bool>> match , string[] includes = null );
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int take, int skip);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int? take, int? skip, Expression<Func<T , object>>orderBy =null ,string orderByDirection = OrderBy.Ascending );
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);


    }
}
