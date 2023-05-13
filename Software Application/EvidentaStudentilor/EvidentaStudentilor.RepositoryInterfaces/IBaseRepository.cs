namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IBaseRepository<T>
    {
        public IQueryable<T> FindAll();

        public void Create(T entity);

        public void Update(T entity);

        public void Delete(T entity);

    }
}
