using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.Repositories
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {

        }

        public new IEnumerable<Profile> FindAll()
        {
            return dbContext.Set<Profile>().ToList();
        }
    }
}
