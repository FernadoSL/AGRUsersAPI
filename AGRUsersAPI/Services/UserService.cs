using AGRUsersAPI.Configuration;
using AllGoRithmFramework.Domain.DataObjects;
using AllGoRithmFramework.Domain.Entities;
using AllGoRithmFramework.Repository.Contexts;
using AllGoRithmFramework.Service.DomainServices;
using AllGoRithmFramework.Service.Factories;
using Microsoft.Extensions.Options;

namespace AGRUsersAPI.Services
{
    public class UserService : AllGoRithmFramework.Service.DomainServices.UserService
    {
        protected UserFactory UserFactory { get; set; }

        protected EncryptService EncryptService { get; set; }

        public UserService(IOptions<EncryptConfiguration> encryptConfiguration, IOptions<DbContextConfiguration> dbContextConfiguration) 
            : base(new BaseContext<User>(dbContextConfiguration.Value.ConnectionString))
        {
            string iv = encryptConfiguration.Value.Iv;
            string key = encryptConfiguration.Value.Key;

            this.UserFactory = new UserFactory();
            this.EncryptService = new EncryptService(key, iv);
        }

        public void RegisterUser(UserDto userDto)
        {
            userDto.Password = this.EncryptService.Encrypt(userDto.Password);
            this.Insert(this.UserFactory.Create(userDto));
        }
    }
}