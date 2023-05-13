using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EvidentaStudentilor.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(EvidentaStudentilorContext dbContext) : base(dbContext)
        {

        }

        IEnumerable<User> IUserRepository.FindAll()
        {
            return dbContext.Set<User>().Include(x => x.Role).Include(x => x.Student).Include(x => x.Teacher).ToList();
        }
    }
}
