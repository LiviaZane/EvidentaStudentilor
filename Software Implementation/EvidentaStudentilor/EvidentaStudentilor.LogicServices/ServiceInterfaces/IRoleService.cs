using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface IRoleService
    {
        public void createRole(Role role);
        public void updateRole(Role role);
        public void deleteRole(Role role);
        public IEnumerable<Role> GetRole();
        public Role GetRole(int id);
        public IEnumerable<Role> GetRole(string a);
        public IEnumerable<Role> GetRole(string a, string b);
    }
}
