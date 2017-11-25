using System.Collections.Generic;
using AGRUsersAPI.DataObjects.Reposnse;
using AGRUsersAPI.Domain.Entities;
using AllGoRithmFramework.Service.Factories;

namespace AGRUsersAPI.Services.Factories
{
    public class UserLogFactory : IBaseFactory<UserLog, UserLogDto>
    {
        public UserLog Create(UserLogDto source)
        {
            return new UserLog(source.UserId);
        }

        public UserLog Create(int userId)
        {
            return new UserLog(userId);
        }

        public IList<UserLog> CreateList(int[] userIdList)
        {
            IList<UserLog> result = new List<UserLog>();

            foreach (int userId in userIdList)
            {
                result.Add(this.Create(userId));
            }

            return result;
        }

        public IList<UserLog> CreateList(IList<UserLogDto> sourceList)
        {
            IList<UserLog> result = new List<UserLog>();

            foreach (UserLogDto source in sourceList)
            {
                result.Add(this.Create(source));
            }

            return result;
        }
    }
}