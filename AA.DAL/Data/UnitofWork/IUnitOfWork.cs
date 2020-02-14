using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace AA.DAL.Data.UnitofWork
{
    public interface IUnitOfWork
    {
        int Commit();
        bool AddEntity<T>(T entidad) where T : class;
        void Update<T>(T entityToUpdate) where T : class;
        void Delete<T>(T entityToUpdate) where T : class;
        void DeleteAll<T>(IEnumerable<T> entity) where T : class;
        List<T> GetAll<T>() where T : class;
        List<T> GetQuery<T>(Expression<Func<T, bool>> where) where T : class;
        T GetById<T>(int id) where T : class;
        void MassiveBulkSave(DataTable dataTable);
        void MassiveDelete(string tableName);
    }
}
