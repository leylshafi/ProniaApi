using ProniaApi.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ProniaApi.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T: BaseEntity,new()
    {
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null,
            bool isDesc = false,
            int skip = 0,
            int take = 0, bool isTracking = true,
            params string[] includes);

        Task<T> GetByIdAsync(int id);
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
        Task SaveChangesAsync();
    }
}
