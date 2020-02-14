using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AA.DAL.Data.Repositories.Base
{
    public interface IRepositoryBase<TModel> where TModel : class
    {
        IQueryable<TModel> GetAll();
        IQueryable<TModel> GetAllWithTracking();
        int GetCount();
        void Add(TModel entityToAdd);
        void Update(TModel entityToUpdate);
        void Delete(TModel entityToDelete);
        void DeleteRelationship(TModel entityToDelete);
        void Delete(Expression<Func<TModel, bool>> where);
        TModel GetById(long id);
        TModel GetById(string id);

        TModel GetById(int id);
        TModel Get(Expression<Func<TModel, bool>> where);
        IQueryable<TModel> GetMany(Expression<Func<TModel, bool>> where);
        void SaveChanges();   
    }
}
