using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.Repositories
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {

        }

        public new IEnumerable<Subject> FindAll()
        {
            return dbContext.Set<Subject>().ToList();
        }
    }
}
