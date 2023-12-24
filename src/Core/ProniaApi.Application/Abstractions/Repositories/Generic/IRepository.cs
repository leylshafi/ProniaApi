using ProniaApi.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ProniaApi.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T: BaseEntity,new()
    {
        IQueryable<T> GetAll(bool isTracking = true,
			bool ignoreQuery = false,
			params string[] includes);
		IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null,
            bool isDesc = false,
            int skip = 0,
            int take = 0, bool isTracking = true,
            bool ignoreQuery = false, 
            params string[] includes);

        Task<T> GetByIdAsync(int id, bool isTracking = true,bool ignoreQuery=false, params string[] includes);
        Task<T> GetByExpressionAsync(Expression<Func<T,bool>> expression, bool isTracking = true,bool ignoreQuery=false, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression,bool ignoreQuery = false);
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
        void SoftDelete(T item);
        void ReverseSoftDelete(T item);
        Task SaveChangesAsync();
    }
}
