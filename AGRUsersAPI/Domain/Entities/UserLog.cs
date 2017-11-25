using System;
using AllGoRithmFramework.Domain.Entities;

namespace AGRUsersAPI.Domain.Entities
{
    public class UserLog : BaseEntity
    {
        public int UserLogId { get; private set; }

        public int UserId { get; private set; }

        public DateTime LogInDateTime { get; private set; }

        public DateTime? LogOutDateTime { get; private set; }

        public bool IsLogged
        {
            get
            {
                return this.LogOutDateTime.HasValue;
            }
        }

        public int MinutesLogged
        {
            get
            {
                if (IsLogged)
                    return 0;
                else
                    return this.LogOutDateTime.Value.Subtract(this.LogInDateTime).Minutes;
            }
        }

        public int HoursLogged
        {
            get
            {
                if (IsLogged)
                    return 0;
                else
                    return this.LogOutDateTime.Value.Subtract(this.LogInDateTime).Hours;
            }
        }

        public UserLog LogIn()
        {
            this.LogInDateTime = DateTime.Now;

            return this;
        }

        public UserLog LogOut()
        {
            this.LogOutDateTime = DateTime.Now;

            return this;
        }

        private UserLog() { }

        public UserLog(int userId)
        {
            this.UserId = userId;
        }
    }
}
