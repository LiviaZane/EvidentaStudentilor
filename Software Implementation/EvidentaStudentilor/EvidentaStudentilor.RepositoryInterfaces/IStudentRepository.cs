using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
         public new IEnumerable<Student> FindAll();

    }
}
