using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IExamRepository : IBaseRepository<Exam>
    {
        public new IEnumerable<Exam> FindAll();
    }
}
