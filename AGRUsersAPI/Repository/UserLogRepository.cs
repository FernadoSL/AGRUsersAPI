using AGRUsersAPI.Domain.Entities;
using AllGoRithmFramework.Repository.Contexts;
using AllGoRithmFramework.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AGRUsersAPI.Repository
{
    public class UserLogRepository : BaseRepository<UserLog>
    {
        public UserLogRepository(BaseContext<UserLog> baseContext) : base(baseContext)
        {
        }

        public int LoggedUsersCount()
        {
            return this.dbSet.Count(ul => ul.IsLogged);
        }

        public UserLog GetLastLogin(int userId)
        {
            return this.dbSet.OrderByDescending(ul => ul.LoginDateTime).FirstOrDefault();
        }

        public UserLog GetLastLogout(int userId)
        {
            return this.dbSet.OrderByDescending(ul => ul.LogoutDateTime).FirstOrDefault();
        }
        
        public int LoginCount(DateTime startDate, DateTime endDate)
        {
            return this.dbSet.Count(ul => ul.LoginDateTime > startDate && ul.LoginDateTime < endDate);
        }

        public List<UserLog> GetAllLogsById(int userId)
        {
            return this.dbSet.Where(ul => ul.UserId == userId).ToList();
        }
    }
}