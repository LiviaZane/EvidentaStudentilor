using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class CurriculaService : ICurriculaService
    {
        private readonly IWrapperRepository _wrapperRepository;

        public CurriculaService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createCurricula(Curricula curricula)
        {
            _wrapperRepository.CurriculaRepository.Create(curricula);
            _wrapperRepository.Save();
        }

        public void updateCurricula(Curricula curricula)
        {
            _wrapperRepository.CurriculaRepository.Update(curricula);
            _wrapperRepository.Save();
        }

        public void deleteCurricula(Curricula curricula)
        {
            _wrapperRepository.CurriculaRepository.Delete(curricula);
            _wrapperRepository.Save();
        }

        public IEnumerable<Curricula> GetCurricula()
        {
            return _wrapperRepository.CurriculaRepository.FindAll();
        }

        public Curricula? GetCurricula(int id)
        {
            return _wrapperRepository.CurriculaRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Profile> GetProfile()
        {
            return _wrapperRepository.ProfileRepository.FindAll();
        }

        public IEnumerable<Subject> GetSubject()
        {
            return _wrapperRepository.SubjectRepository.FindAll();
        }
    }
}
