using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EvidentaStudentilor.Repositories
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {
        }
        public new IEnumerable<Schedule> FindAll()
        {
            return dbContext.Set<Schedule>().Include(x => x.Profile).ToList();

        }
    }
}
