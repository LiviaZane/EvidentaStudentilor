using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EvidentaStudentilor.Repositories
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {
        }
        public new IEnumerable<Exam> FindAll()
        {
            return dbContext.Set<Exam>().Include(x => x.Teacher).Include(x => x.Profile).Include(x => x.Subject).Include(x => x.Grades);
        }
    }
}
