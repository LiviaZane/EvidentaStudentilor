using EvidentaStudentilor.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EvidentaStudentilor.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly EvidentaStudentilorContext dbContext;

        public BaseRepository(EvidentaStudentilorContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IQueryable<T> FindAll()
        {
            return this.dbContext.Set<T>().AsNoTracking();
        }

        public void Create(T entity)
        {
            this.dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.dbContext.Set<T>().Remove(entity);
        }
    }
}
