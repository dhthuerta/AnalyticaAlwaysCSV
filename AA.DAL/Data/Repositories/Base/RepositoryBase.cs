using AA.DAL.Data.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AA.DAL.Data.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable where TEntity : class
    {

        private readonly DbSet<TEntity> _dbset;
        private AA_BBDDContext _mainContext;

        protected IFactory DataFactory
        {
            get;
            private set;
        }

        protected AA_BBDDContext MainContext
        {
            get { return _mainContext ?? (_mainContext = DataFactory.GetMainContext()); }
        }
        protected RepositoryBase(IFactory dataFactory)
        {
            DataFactory = dataFactory;
            _dbset = MainContext.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }

        public virtual int GetCount()
        {
            return _dbset.AsNoTracking().AsQueryable().Count();
        }
        public virtual IQueryable<TEntity> GetAllWithTracking()
        {
            return _dbset.AsQueryable();
        }
        public virtual void Add(TEntity entityToAdd)
        {

            _dbset.Add(entityToAdd);
        }

        public virtual void AddRange(IEnumerable<TEntity> entitiesToAdd)
        {
            _mainContext.Set<TEntity>().AddRange(entitiesToAdd);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            EntityEntry<TEntity> entry = _mainContext.Entry(entityToUpdate);
            if (entry.State == EntityState.Detached)
            {
                _mainContext.Set<TEntity>().Attach(entityToUpdate);
            }
            _mainContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            _dbset.Attach(entityToDelete);
            _dbset.Remove(entityToDelete);
        }

        public virtual void DeleteRelationship(TEntity entityToDelete)
        {
            EntityEntry<TEntity> entry = _mainContext.Entry(entityToDelete);
            _dbset.Remove(entityToDelete);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> objects = _dbset.Where<TEntity>(where);

            if (objects != null && objects.Count() > 0)
                _mainContext.Set<TEntity>().RemoveRange(objects);
        }

        public virtual TEntity GetById(long id)
        {
            TEntity entity = _mainContext.Set<TEntity>().Find(id);
            return entity;
        }
        public virtual TEntity GetById(string id)
        {
            TEntity entity = _mainContext.Set<TEntity>().Find(id);
            return entity;
        }
        public virtual TEntity GetById(int id)
        {
            TEntity entity = _mainContext.Set<TEntity>().Find(id);
            return entity;
        }

        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.AsNoTracking().Where(where).AsQueryable();
        }

        public virtual void SaveChanges()
        {
            _mainContext.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<TEntity>();
        }

        public void Dispose()
        {

        }
    }
}
