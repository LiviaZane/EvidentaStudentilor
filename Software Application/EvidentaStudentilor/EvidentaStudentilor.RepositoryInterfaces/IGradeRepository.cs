using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IGradeRepository : IBaseRepository<Grade>
    {
        public new IEnumerable<Grade> FindAll();
    }
}
