using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;

namespace EvidentaStudentilor.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {
        }

        public new IEnumerable<Role> FindAll()
        {
            return dbContext.Set<Role>().ToList();
        }
    }
}
