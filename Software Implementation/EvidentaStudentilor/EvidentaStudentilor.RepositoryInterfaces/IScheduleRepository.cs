using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.RepositoryInterfaces
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        public new IEnumerable<Schedule> FindAll();
    }
}
