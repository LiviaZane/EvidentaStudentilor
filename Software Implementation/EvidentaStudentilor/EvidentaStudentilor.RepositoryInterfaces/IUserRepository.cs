using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public new IEnumerable<User> FindAll();
    }
}
