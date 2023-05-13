using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class UserService : IUserService
    {
        private readonly IWrapperRepository _wrapperRepository;

        public UserService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createUser(User user)
        {

            _wrapperRepository.UserRepository.Create(user);
            _wrapperRepository.Save();
        }

        public void deleteUser(User user)
        {
            _wrapperRepository.UserRepository.Delete(user);
            _wrapperRepository.Save();
        }

        public void updateUser(User user)
        {
            _wrapperRepository.UserRepository.Update(user);
            _wrapperRepository.Save();
        }

        public IEnumerable<User> GetUser()
        {
            return _wrapperRepository.UserRepository.FindAll();
        }

        public User? GetUser(int id)
        {
            return _wrapperRepository.UserRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }

        public User? GetUserByEmailPassword(string email, string password)
        {
            return _wrapperRepository.UserRepository.FindAll().Where(a => a.Email.Equals(email) && a.Paswword.Equals(password)).FirstOrDefault();
        }

        public User? GetUserByEmail(string email)
        {
            return _wrapperRepository.UserRepository.FindAll().Where(a => a.Email.Equals(email)).FirstOrDefault();
        }

        public int GetUserIdByEmail(string email)
        {
            return _wrapperRepository.UserRepository.FindAll().Where(a => a.Email.Equals(email)).FirstOrDefault().Id;
        }

        public IEnumerable<Role> GetRole()
        {
            return _wrapperRepository.RoleRepository.FindAll();
        }

        public IEnumerable<Role> GetRole(string a, string b)
        {
            return _wrapperRepository.RoleRepository.FindAll().Where(x => x.Name == a || x.Name == b);
        }

        private int GetRoleId(string a)
        {
            return _wrapperRepository.RoleRepository.FindAll().FirstOrDefault(x => x.Name == a).Id;
        }

        public IEnumerable<User> GetUsersRole(string a)
        {
            int aa = _wrapperRepository.RoleRepository.FindAll().FirstOrDefault(x => x.Name == a).Id;
            return _wrapperRepository.UserRepository.FindAll().Where(x => x.RoleId == aa).ToList();
        }

        public IEnumerable<User> GetUsersRole(string a, string b)
        {
            int aa = _wrapperRepository.RoleRepository.FindAll().FirstOrDefault(x => x.Name == a).Id;
            int bb = _wrapperRepository.RoleRepository.FindAll().FirstOrDefault(x => x.Name == b).Id;
            return _wrapperRepository.UserRepository.FindAll().Where(x => x.RoleId == aa || x.RoleId == bb).ToList();
        }

        public IEnumerable<Student> GetStudent()
        {
            return _wrapperRepository.StudentRepository.FindAll();
        }

        public IEnumerable<Teacher> GetTeacher()
        {
            return _wrapperRepository.TeacherRepository.FindAll();
        }
    }
}
