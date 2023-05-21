using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class BaseService : IBaseService
    {
        private IWrapperRepository _wrapperRepository;
        public BaseService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }
        public void Save()
        {
            _wrapperRepository.Save();

        }
    }
}
