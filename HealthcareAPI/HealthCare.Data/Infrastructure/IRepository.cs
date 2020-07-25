using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Data.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetById(long id);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> where);
        Task<IEnumerable<TEntity>> GetAll();
        Task<int> GetAllCount();
        Task<int> GetCount(Expression<Func<TEntity, bool>> where);
        Task<IEnumerable<TEntity>> Fetch(Expression<Func<TEntity, bool>> where);
    }
}
