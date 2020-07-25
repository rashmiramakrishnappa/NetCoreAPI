using HealthCare.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Data.Infrastructure
{
    public class RepositoryBase<TEntity> : Disposable, IRepository<TEntity> where TEntity : class
    {
        private readonly HealthcareDBContext _dataContext;
        private DbSet<TEntity> Dbset
        {
            get { return _dataContext.Set<TEntity>(); }
        }
        public RepositoryBase(HealthcareDBContext dbContext)
        {
            _dataContext = dbContext;
        }
        public virtual TEntity Create(TEntity entity)
        {
            Dbset.Add(entity);
            return entity;
        }
        public virtual void Update(TEntity entity)
        {
            Dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(TEntity entity)
        {
            Dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            var objects = Dbset.Where(where).AsEnumerable();
            foreach (var obj in objects)
                Dbset.Remove(obj);
        }
        public virtual async Task<TEntity> GetById(long id)
        {
            return await Dbset.FindAsync(id);
        }
        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> where)
        {
            return await Dbset.FirstOrDefaultAsync(where);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Dbset.ToListAsync();
        }
        public async Task<int> GetAllCount()
        {
            return await Dbset.CountAsync();
        }
        public async Task<int> GetCount(Expression<Func<TEntity, bool>> @where)
        {
            return await Dbset.CountAsync(where);
        }
        public async Task<IEnumerable<TEntity>> Fetch(Expression<Func<TEntity, bool>> where)
        {
            return await Dbset.Where(where).ToListAsync();
        }
    }
}
