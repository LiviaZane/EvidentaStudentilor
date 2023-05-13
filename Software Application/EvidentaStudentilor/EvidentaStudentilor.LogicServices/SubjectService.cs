using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class SubjectService : ISubjectService
    {
        private readonly IWrapperRepository _wrapperRepository;

        public SubjectService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createSubject(Subject subject)
        {

            _wrapperRepository.SubjectRepository.Create(subject);
            _wrapperRepository.Save();
        }

        public void deleteSubject(Subject subject)
        {
            _wrapperRepository.SubjectRepository.Delete(subject);
            _wrapperRepository.Save();
        }

        public void updateSubject(Subject subject)
        {
            _wrapperRepository.SubjectRepository.Update(subject);
            _wrapperRepository.Save();
        }


        public IEnumerable<Subject> GetSubject()
        {
            return _wrapperRepository.SubjectRepository.FindAll();
        }

        public Subject GetSubject(int id)
        {
            return _wrapperRepository.SubjectRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
