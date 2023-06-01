using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface IStudentService
    {
        public void createStudent(Student student);
        public void updateStudent(Student student);
        public void deleteStudent(Student student);
        public Student GetStudentByUserId(int userId);
        public IEnumerable<Student> GetStudent();
        public Student GetStudent(int id);
        public IEnumerable<User> GetUsersWithoutStudent();
        public IEnumerable<User> GetUser();
        public IEnumerable<Profile> GetProfile();
    }
}
