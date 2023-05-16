using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IWrapperRepository _wrapperRepository;

        public DepartmentService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }
        public void createDepartment(Department department)
        {

            _wrapperRepository.DepartmentRepository.Create(department);
            _wrapperRepository.Save();
        }
        public void deleteDepartment(Department department)
        {
            _wrapperRepository.DepartmentRepository.Delete(department);
            _wrapperRepository.Save();
        }
        public void updateDepartment(Department department)
        {
            _wrapperRepository.DepartmentRepository.Update(department);
            _wrapperRepository.Save();
        }
        public IEnumerable<Department> GetDepartment()
        {
            return _wrapperRepository.DepartmentRepository.FindAll();
        }
        public Department? GetDepartment(int id)
        {
            return _wrapperRepository.DepartmentRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
