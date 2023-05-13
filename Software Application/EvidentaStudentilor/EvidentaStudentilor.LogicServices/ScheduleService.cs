using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class ScheduleService : IScheduleService
    {
        private readonly IWrapperRepository _wrapperRepository;
        public ScheduleService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createSchedule(Schedule schedule)
        {
            _wrapperRepository.ScheduleRepository.Create(schedule);
            _wrapperRepository.Save();
        }

        public void updateSchedule(Schedule schedule)
        {
            _wrapperRepository.ScheduleRepository.Update(schedule);
            _wrapperRepository.Save();
        }

        public void deleteSchedule(Schedule schedule)
        {
            _wrapperRepository.ScheduleRepository.Delete(schedule);
            _wrapperRepository.Save();
        }

        public IEnumerable<Schedule> GetSchedule()
        {
            return _wrapperRepository.ScheduleRepository.FindAll();
        }


        public Schedule GetSchedule(int id)
        {
            return _wrapperRepository.ScheduleRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Profile> GetProfile()
        {
            return _wrapperRepository.ProfileRepository.FindAll();
        }
    }
}
