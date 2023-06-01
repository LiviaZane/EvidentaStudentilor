using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.LogicServices
{
    public class ProfileService : IProfileService
    {
        private readonly IWrapperRepository _wrapperRepository;

        public ProfileService(IWrapperRepository wrapperRepository)
        {
            _wrapperRepository = wrapperRepository;
        }

        public void createProfile(Profile profile)
        {

            _wrapperRepository.ProfileRepository.Create(profile);
            _wrapperRepository.Save();
        }

        public void updateProfile(Profile profile)
        {
            _wrapperRepository.ProfileRepository.Update(profile);
            _wrapperRepository.Save();
        }

        public void deleteProfile(Profile profile)
        {
            _wrapperRepository.ProfileRepository.Delete(profile);
            _wrapperRepository.Save();
        }

        public IEnumerable<Profile> GetProfile()
        {
            return _wrapperRepository.ProfileRepository.FindAll();
        }


        public Profile GetProfile(int id)
        {
            return _wrapperRepository.ProfileRepository.FindAll().FirstOrDefault(x => x.Id == id);
        }
    }
}
