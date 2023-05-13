using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        public new IEnumerable<Profile> FindAll();
    }
}
