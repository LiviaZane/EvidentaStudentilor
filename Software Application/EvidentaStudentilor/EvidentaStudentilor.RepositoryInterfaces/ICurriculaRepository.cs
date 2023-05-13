using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface ICurriculaRepository : IBaseRepository<Curricula>
    {
        public new IEnumerable<Curricula> FindAll();
    }
}
