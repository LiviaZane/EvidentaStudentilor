using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class StudentService : IStudentService
    {
        private readonly IWrapperRepository _wrapperRepository;

        public StudentService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createStudent(Student student)
        {
            _wrapperRepository.StudentRepository.Create(student);
            _wrapperRepository.Save();
        }

        public void updateStudent(Student student)
        {
            _wrapperRepository.StudentRepository.Update(student);
            _wrapperRepository.Save();
        }

        public void deleteStudent(Student student)
        {
            _wrapperRepository.StudentRepository.Delete(student);
            _wrapperRepository.Save();
        }

        public IEnumerable<Student> GetStudent()
        {
            return _wrapperRepository.StudentRepository.FindAll();
        }

        public Student GetStudent(int id) 
        {
            return _wrapperRepository.StudentRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }

        public Student GetStudentByUserId(int userId)
        {
            return _wrapperRepository.StudentRepository.FindAll().FirstOrDefault(x => x.UserId == userId);
        }

        public IEnumerable<User> GetUsersWithoutStudent()
        {
            var list = _wrapperRepository.StudentRepository.FindAll();
            var list1 = _wrapperRepository.UserRepository.FindAll().Where(x => x.RoleId == 1); // user role for student is 1
            var list2 = list1.Where(x => list.All(y => y.UserId != x.Id));  
            return list2;
        }

        public IEnumerable<User> GetUser()
        {
            return _wrapperRepository.UserRepository.FindAll();
        }

        public IEnumerable<Profile> GetProfile()
        {
            return _wrapperRepository.ProfileRepository.FindAll();
        }
    }
}
