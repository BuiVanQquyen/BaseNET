using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using ExCore.RFS.Utils.Common;
using System.Data;

namespace ExCore.RFS.Utils.Repository
{
    public class Repository<TContext> : IRepository where TContext: DbContext 
    {
        protected TContext _dbContext;
        public Repository(TContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TEntity> All<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public virtual int Count<TEntity>(params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            return _dbContext.Set<TEntity>().WhereMany(predicates).Count();
        }

        public async virtual Task<int> CountAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            return await _dbContext.Set<TEntity>()
                .WhereMany(predicates)
                .CountAsync();
        }

        public virtual TEntity Find<TEntity>(params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            return _dbContext.Set<TEntity>().WhereMany(predicates).FirstOrDefault();
        }

        public virtual TEntity Find<TEntity>(object id) where TEntity : class
        {
            if (id is object[])
            {
                return _dbContext.Set<TEntity>().Find(id as object[]);
            }
            else
            {
                return _dbContext.Set<TEntity>().Find(id);
            }
        }

        public async virtual Task<TEntity> FindAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().WhereMany(predicates).FirstOrDefaultAsync();
        }

        public async virtual Task<TEntity> FindAsync<TEntity>(object id) where TEntity : class
        {
            if (id is object[])
            {
                return await _dbContext.Set<TEntity>().FindAsync(id as object[]);
            }
            else
            {
                var entity = await _dbContext.Set<TEntity>().FindAsync(id);
                return entity;
            }
        }

        public virtual IQueryable<TEntity> Filter<TEntity>(params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            return _dbContext.Set<TEntity>().WhereMany(predicates);
        }

        public bool Contain<TEntity>(params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            return _dbContext.Set<TEntity>().WhereMany(predicates).Any();
        }

        public async Task<bool> ContainAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().WhereMany(predicates).AnyAsync();
        }

        #region CRUD

        public virtual void Create<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                _dbContext.Set<TEntity>().Add(entity);
            }
            _dbContext.SaveChanges();
        }

        public async virtual Task CreateAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                _dbContext.Set<TEntity>().Add(entity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public virtual int Delete<TEntity, TKey>(TKey id) where TEntity : class
        {
            var entity = Find<TEntity>(id);

            return Delete(entity);
        }

        public virtual int Delete<TEntity, TKey>(TKey[] ids) where TEntity : class
        {
            foreach (var id in ids)
            {
                var entity = Find<TEntity>(id);
                
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return _dbContext.SaveChanges();
        }

        public virtual int Delete<TEntity>(TEntity entity)  where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);

            return _dbContext.SaveChanges();
        }

        public virtual int Delete<TEntity>(TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return _dbContext.SaveChanges();
        }

        public virtual int Delete<TEntity>(params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            var entities = Filter<TEntity>(predicates).ToArray();

            foreach (var entity in entities)
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return _dbContext.SaveChanges();
        }

        public async virtual Task<int> DeleteAsync<TEntity, TKey>(TKey id) where TEntity : class
        {
            var entity = await FindAsync<TEntity>(id);

            return await DeleteAsync(entity);
        }

        public async virtual Task<int> DeleteAsync<TEntity, TKey>(params TKey[] ids) where TEntity : class
        {
            foreach (var id in ids)
            {
                var entity = await FindAsync<TEntity>(id);
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> DeleteAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<int> DeleteAsync<TEntity>(params Expression<Func<TEntity, bool>>[] predicates) where TEntity : class
        {
            var entities = await Filter<TEntity>(predicates).ToArrayAsync();

            foreach (var entity in entities)
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public virtual int Update<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                var entry = _dbContext.Entry(entity);
            }

            return _dbContext.SaveChanges();
        }

        public async virtual Task<int> UpdateAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                var entry = _dbContext.Entry(entity);
            }

            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async virtual Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual int ExecuteNonQuery(string sql, params object[] sqlParams)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql, sqlParams);
        }

        public TDbContext GetDbContext<TDbContext>() where TDbContext : class
        {
            return this._dbContext as TDbContext;
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public virtual IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _dbContext.Database.BeginTransaction(isolationLevel);
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
        }

    }
}
