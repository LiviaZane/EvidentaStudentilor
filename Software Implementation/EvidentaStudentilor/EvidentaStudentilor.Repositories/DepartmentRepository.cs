using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {

        }
        public new IEnumerable<Department> FindAll()
        {
            return dbContext.Set<Department>();

        }
    }
}
