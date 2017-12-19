using AllGoRithmFramework.Domain.DataObjects;
using System;

namespace AGRUsersAPI.DataObjects.Reposnse
{
    public class UserLogDto : BaseDto
    {
        public int UserLogId { get; set; }

        public int UserId { get; set; }

        public DateTime LogInDateTime { get; set; }

        public DateTime? LogOutDateTime { get; set; }
    }
}