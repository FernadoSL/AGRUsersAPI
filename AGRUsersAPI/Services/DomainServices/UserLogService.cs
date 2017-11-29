using AGRUsersAPI.Domain.Entities;
using AGRUsersAPI.Repository;
using AGRUsersAPI.Services.Factories;
using AllGoRithmFramework.Repository.Contexts;
using AllGoRithmFramework.Service.DomainServices;

namespace AGRUsersAPI.Services.DomainServices
{
    public class UserLogService : BaseDomainService<UserLog>
    {
        protected UserLogFactory UserLogFactory { get; set; }

        protected UserLogRepository UserLogRepository { get; set; }

        public UserLogService(BaseContext<UserLog> dbContext) : base(dbContext)
        {
            this.UserLogRepository = new UserLogRepository(dbContext);
            this.UserLogFactory = new UserLogFactory();
        }

        public int LoggedInUsersCount()
        {
            return this.UserLogRepository.LoggedInUsersCount();
        }

        public void LogLogin(int userId)
        {
            if(this.UserLogRepository.IsLogged(userId))
                this.Insert(this.UserLogFactory.Create(userId).LogIn());
        }

        public bool LogLogout(int userId)
        {
            UserLog userLog = this.UserLogRepository.GetLastLogin(userId);
            
            if(userLog != null)
                this.Update(userLog.LogOut());

            return userLog != null;
        }
    }
}