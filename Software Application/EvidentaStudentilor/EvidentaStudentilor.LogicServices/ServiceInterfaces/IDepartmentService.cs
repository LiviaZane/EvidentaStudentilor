using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface IDepartmentService
    {
        void createDepartment(Department department);
        void updateDepartment(Department department);
        void deleteDepartment(Department department);
        IEnumerable<Department> GetDepartment();
        public Department? GetDepartment(int id);
    }
}
