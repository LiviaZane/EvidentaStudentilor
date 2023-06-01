using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class TeacherService : ITeacherService
    {
        private readonly IWrapperRepository _wrapperRepository;

        public TeacherService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createTeacher(Teacher teacher)
        {

            _wrapperRepository.TeacherRepository.Create(teacher);
            _wrapperRepository.Save();
        }

        public void deleteTeacher(Teacher teacher)
        {
            _wrapperRepository.TeacherRepository.Delete(teacher);
            _wrapperRepository.Save();
        }

        public void updateTeacher(Teacher teacher)
        {
            _wrapperRepository.TeacherRepository.Update(teacher);
            _wrapperRepository.Save();
        }

        public IEnumerable<Teacher> GetTeacher()
        {
            return _wrapperRepository.TeacherRepository.FindAll().ToList();
        }

        public Teacher? GetTeacher(int id)
        {
            return _wrapperRepository.TeacherRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }

        public Teacher? GetTeacherByUserId(int userId)
        {
            return _wrapperRepository.TeacherRepository.FindAll().FirstOrDefault(x => x.UserId == userId);
        }

        public IEnumerable<User> GetUser()
        {
            return _wrapperRepository.UserRepository.FindAll();
        }

        public IEnumerable<Department> GetDepartment()
        {
            return _wrapperRepository.DepartmentRepository.FindAll();
        }

        public string GetDepartment(int id)
        {
            return _wrapperRepository.DepartmentRepository.FindAll().FirstOrDefault(x => x.Id == id).Name;
        }

        public IEnumerable<User> GetUsersWithoutTeacher()
        {
            var list = _wrapperRepository.TeacherRepository.FindAll();
            var list1 = _wrapperRepository.UserRepository.FindAll().Where(x => x.RoleId == 2); //user role for Teacher is 2
            var list2 = list1.Where(x => list.All(y => y.UserId != x.Id));
            return list2;
        }
    }
}
