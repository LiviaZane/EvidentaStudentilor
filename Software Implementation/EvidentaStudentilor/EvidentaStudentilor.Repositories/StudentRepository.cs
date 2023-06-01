using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EvidentaStudentilor.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {
        }
        public new IEnumerable<Student> FindAll()
        {
            return dbContext.Set<Student>().Include(s => s.Profile).Include(y => y.User);

        }
        
    }
}
