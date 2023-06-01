using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        public new IEnumerable<Teacher> FindAll();
    }
}
