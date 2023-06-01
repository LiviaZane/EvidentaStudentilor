using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
        public new IEnumerable<Subject> FindAll();
    }
}
