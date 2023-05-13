using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface ITeacherService
    {
        public void createTeacher(Teacher teacher);
        public void updateTeacher(Teacher teacher);
        public void deleteTeacher(Teacher teacher);
        public IEnumerable<Teacher> GetTeacher();
        public Teacher GetTeacher(int id);
        public Teacher? GetTeacherByUserId(int userId);
        public IEnumerable<User> GetUser();
        public IEnumerable<User> GetUsersWithoutTeacher();
        public IEnumerable<Department> GetDepartment();
        public string GetDepartment(int id);
    }
}
