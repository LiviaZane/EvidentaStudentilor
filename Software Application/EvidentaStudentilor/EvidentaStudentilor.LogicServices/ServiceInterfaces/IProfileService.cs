using EvidentaStudentilor.DataModel;

namespace EvidentaStudentilor.LogicServices.ServiceInterfaces
{
    public interface IProfileService
    {
        public void createProfile(Profile profile);
        public void updateProfile(Profile profile);
        public void deleteProfile(Profile profile);
        public IEnumerable<Profile> GetProfile();
        public Profile GetProfile(int id);
    }
}
