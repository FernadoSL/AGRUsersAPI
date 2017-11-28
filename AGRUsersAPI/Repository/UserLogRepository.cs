using System.Linq;
using AGRUsersAPI.Domain.Entities;
using AllGoRithmFramework.Repository.Contexts;
using AllGoRithmFramework.Repository.Repositories;

namespace AGRUsersAPI.Repository
{
    public class UserLogRepository : BaseRepository<UserLog>
    {
        public UserLogRepository(BaseContext<UserLog> baseContext) : base(baseContext)
        {
        }

        public int LoggedInUsersCount()
        {
            return this.dbSet.Count(ul => ul.IsLogged);
        }

        public UserLog GetLastLogin(int userId)
        {
            return this.dbSet.OrderByDescending(ul => ul.LogInDateTime).FirstOrDefault();
        }

        public UserLog GetLastLogout(int userId)
        {
            return this.dbSet.OrderByDescending(ul => ul.LogOutDateTime).FirstOrDefault();
        }

        public bool IsLogged(int userId)
        {
            return this.dbSet.Any(ul => ul.IsLogged);
        }
    }
}