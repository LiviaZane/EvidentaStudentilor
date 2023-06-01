using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EvidentaStudentilor.Repositories
{
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {
        }
        public new IEnumerable<Grade> FindAll()
        {
            return dbContext.Set<Grade>().Include(x => x.Teacher).Include(x => x.Profile).Include(x => x.Subject).Include(x => x.Student).ToList();

        }
    }
}
