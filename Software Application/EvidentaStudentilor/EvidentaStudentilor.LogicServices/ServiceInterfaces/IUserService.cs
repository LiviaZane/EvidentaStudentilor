using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface IUserService
    {
        public void createUser(User user);
        public void updateUser(User user);
        public void deleteUser(User user);
        public IEnumerable<User> GetUser();
        public User GetUser(int id);
        public User? GetUserByEmailPassword(string email, string password);
        public User? GetUserByEmail(string email);
        public int GetUserIdByEmail(string email);
        public IEnumerable<Role> GetRole();
        public IEnumerable<Role> GetRole(string a, string b);
        public IEnumerable<User> GetUsersRole(string a);
        public IEnumerable<User> GetUsersRole(string a, string b);
        public IEnumerable<Student> GetStudent();
        public IEnumerable<Teacher> GetTeacher();
    }
}
