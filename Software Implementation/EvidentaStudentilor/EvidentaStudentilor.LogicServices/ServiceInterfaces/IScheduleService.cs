using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface IScheduleService
    {
        public void createSchedule(Schedule schedule);
        public void updateSchedule(Schedule schedule);
        public void deleteSchedule(Schedule schedule);

        public IEnumerable<Schedule> GetSchedule();
        public Schedule GetSchedule(int id);
        public IEnumerable<Profile> GetProfile();
    }
}
