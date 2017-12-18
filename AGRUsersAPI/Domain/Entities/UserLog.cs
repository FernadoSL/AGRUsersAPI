using System;
using AllGoRithmFramework.Domain.Entities;

namespace AGRUsersAPI.Domain.Entities
{
    public class UserLog : BaseEntity
    {
        public int UserLogId { get; private set; }

        public int UserId { get; private set; }

        public DateTime LoginDateTime { get; private set; }

        public DateTime? LogoutDateTime { get; private set; }

        public bool IsLogged
        {
            get
            {
                return this.LogoutDateTime.HasValue;
            }
        }

        public int MinutesLogged
        {
            get
            {
                if (IsLogged)
                    return DateTime.Now.Subtract(this.LoginDateTime).Minutes;
                else
                    return this.LogoutDateTime.Value.Subtract(this.LoginDateTime).Minutes;
            }
        }

        public int HoursLogged
        {
            get
            {
                if (IsLogged)
                    return DateTime.Now.Subtract(this.LoginDateTime).Hours;
                else
                    return this.LogoutDateTime.Value.Subtract(this.LoginDateTime).Hours;
            }
        }

        public UserLog LogIn()
        {
            this.LoginDateTime = DateTime.Now;

            return this;
        }

        public UserLog LogOut()
        {
            this.LogoutDateTime = DateTime.Now;

            return this;
        }

        private UserLog() { }

        public UserLog(int userId)
        {
            this.UserId = userId;
        }
    }
}
