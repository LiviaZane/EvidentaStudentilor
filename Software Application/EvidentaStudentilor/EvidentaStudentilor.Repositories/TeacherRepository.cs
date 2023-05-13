using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EvidentaStudentilor.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {

        }
        public new IEnumerable<Teacher> FindAll()
        {
            return dbContext.Set<Teacher>().Include(s => s.Department).Include(s => s.User);

        }
    }
}
