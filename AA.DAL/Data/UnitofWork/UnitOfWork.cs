using AA.CrossDomain;
using AA.DAL.Data.Factory;
using AA.DAL.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AA.DAL.Data.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private AA_BBDDContext _mainContext;
        private readonly IFactory _dataFactory;
        private readonly IConfiguration config;
        protected AA_BBDDContext MainContext => _mainContext ?? (_mainContext = _dataFactory.GetMainContext());


        public UnitOfWork(IFactory dataFactory, IConfiguration _config)
        {
            _dataFactory = dataFactory;
            config = _config;
        }
        public int Commit()
        {
            return MainContext.SaveChanges();
        }
        public void MassiveBulkSave(DataTable dataTable)
        {
            using (var scope = Helper.CreateTransactionScope(int.Parse(config[Constants.GEN_TIMEOUT_KEY])))
            {
                SqlConnection sqlCon = new SqlConnection(_dataFactory.GetConnString());
                sqlCon.Open();
                using (SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlCon))
                {
                    bulkInsert.DestinationTableName = Constants.MAIN_TABLE;
                    bulkInsert.BulkCopyTimeout = int.Parse(config[Constants.GEN_TIMEOUT_KEY]);

                    foreach (var column in dataTable.Columns)
                        bulkInsert.ColumnMappings.Add(column.ToString(), column.ToString());

                    bulkInsert.WriteToServer(dataTable);
                }

                sqlCon.Close();

                scope.Complete();
            }

        }

        public void MassiveDelete(string tableName)
        {
            using (var scope = Helper.CreateTransactionScope(int.Parse(config[Constants.GEN_TIMEOUT_KEY])))
            {
                MainContext.Database.SetCommandTimeout(TimeSpan.FromSeconds(int.Parse(config[Constants.GEN_TIMEOUT_KEY])));
                MainContext.Database.ExecuteSqlRaw(string.Format("DELETE FROM {0}", tableName));

                scope.Complete();
            }             
        }
        public bool AddEntity<T>(T entidad) where T : class
        {
            MainContext.Set<T>().Add(entidad);

            return true;
        }
        public void Update<T>(T entityToUpdate) where T : class
        {
            EntityEntry entry = MainContext.Entry(typeof(T));
            if (entry.State == EntityState.Detached)
            {
                MainContext.Set<T>().Attach(entityToUpdate);
            }
            entry.State = EntityState.Modified;
        }
        public void Delete<T>(T entity) where T : class
        {
            MainContext.Set<T>().Remove(entity);
        }
        public void DeleteAll<T>(IEnumerable<T> entity) where T : class
        {
            MainContext.Set<T>().RemoveRange(entity);
        }
        public List<T> GetAll<T>() where T : class
        {
            return MainContext.Set<T>().AsNoTracking().Cast<T>().ToList();
        }
        public List<T> GetQuery<T>(Expression<Func<T, bool>> where) where T : class
        {
            var query = MainContext.Set<T>().Where(where);
            return query.ToList();
        }
        public T GetById<T>(int id) where T : class
        {
            var entity = (T)MainContext.Set<T>().Find(id);
            return entity;
        }

    }


}
