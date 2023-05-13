using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class RoleService : IRoleService
    {
        private readonly IWrapperRepository _wrapperRepository;

        public RoleService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createRole(Role role)
        {

            _wrapperRepository.RoleRepository.Create(role);
            _wrapperRepository.Save();
        }

        public void deleteRole(Role role)
        {
            _wrapperRepository.RoleRepository.Delete(role);
            _wrapperRepository.Save();
        }

        public void updateRole(Role role)
        {
            _wrapperRepository.RoleRepository.Update(role);
            _wrapperRepository.Save();
        }

        public IEnumerable<Role> GetRole()
        {
            return _wrapperRepository.RoleRepository.FindAll();
        }


        public Role GetRole(int id)
        {
            return _wrapperRepository.RoleRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Role> GetRole(string a)
        {
            return _wrapperRepository.RoleRepository.FindAll().Where(x => x.Name == a);
        }

        public IEnumerable<Role> GetRole(string a, string b)
        {
            return _wrapperRepository.RoleRepository.FindAll().Where(x => x.Name == a || x.Name == b);
        }
    }
}
