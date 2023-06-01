using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        public new IEnumerable<Role> FindAll();
    }
}
