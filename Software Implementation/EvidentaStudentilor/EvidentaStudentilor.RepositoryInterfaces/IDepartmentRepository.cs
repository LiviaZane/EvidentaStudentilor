using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        public new IEnumerable<Department> FindAll();
    }
}
