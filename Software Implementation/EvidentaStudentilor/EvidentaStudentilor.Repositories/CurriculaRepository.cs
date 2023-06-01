using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EvidentaStudentilor.Repositories
{
    public class CurriculaRepository : BaseRepository<Curricula>, ICurriculaRepository
    {
        public CurriculaRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {
        }

        public new IEnumerable<Curricula> FindAll()
        {
            return dbContext.Set<Curricula>().Include(x => x.Profile).Include(c => c.Subject).ToList();

        }
    }
}
