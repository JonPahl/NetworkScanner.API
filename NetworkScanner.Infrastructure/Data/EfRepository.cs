using LiteDB;
using NetworkScanner.SharedKernel;
using NetworkScanner.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkScanner.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        private readonly NetworkContext _dbContext;
        protected string Table { get; set; }
        protected string DbName { get; set; }

        public EfRepository(NetworkContext dbContext) => _dbContext = dbContext;

        public void SetTable(string name) => Table = name;

        public T GetById<T>(int id) where T : BaseEntity
        {
            using LiteDatabase liteDatabase = new LiteDatabase(DbName);
            var col = liteDatabase.GetCollection<T>(Table);
            var result = col.FindAll().FirstOrDefault(x => x.Id.Equals(id));
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            using LiteDatabase liteDatabase = new LiteDatabase(DbName);
            var col = liteDatabase.GetCollection<T>(Table);

            //var results=await Task.Run(_=>col.FindAll().ToList()).ConfigureAwait(false);

            var result = await Task.Run(()
                => col.FindAll().FirstOrDefault(x => x.Id.Equals(id)))
                .ConfigureAwait(false);

            return (T)Convert.ChangeType(result, typeof(T));

            //var result = await Task.Run(()
            //    => col.Find(x => x.Id.Equals(id)).FirstOrDefault());
            //var results = col.Where(x => x.Id.Equals(id));            
            //return _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            using LiteDatabase liteDatabase = (LiteDatabase)_dbContext.Database;
            var col = liteDatabase.GetCollection<T>(Table);
            col.EnsureIndex(x => x.Id, true);

            var results = await Task.Run(() => col.FindAll().ToList())
                .ConfigureAwait(false);
            return (List<T>)Convert.ChangeType(results, typeof(List<T>));
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                using LiteDatabase liteDatabase = (LiteDatabase)_dbContext.Database;
                var col = liteDatabase.GetCollection<T>(Table);
                col.EnsureIndex(x => x.Id, true);

                var result = await Task.Run(() => col.Insert(entity)).ConfigureAwait(false);
                return (T)Convert.ChangeType(result.RawValue, typeof(T));
            }
            catch (LiteException ex)
            {
                Console.WriteLine(ex.Message, ex);
                return (T)Convert.ChangeType(-1, typeof(T));
            }
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            using LiteDatabase liteDatabase = (LiteDatabase)_dbContext.Database;
            var col = liteDatabase.GetCollection<T>(Table);
            var result = await Task.Run(() => col.Update(entity)).ConfigureAwait(false);
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            using LiteDatabase liteDatabase = (LiteDatabase)_dbContext.Database;
            var col = liteDatabase.GetCollection<T>(Table);
            var result = await Task.Run(() => col.Delete(entity.Id)).ConfigureAwait(false);
        }
    }
}
