using System;
using System.Net.Mail;
using AGRUsersAPI.Configuration;
using AllGoRithmFramework.Domain.DataObjects;
using AllGoRithmFramework.Domain.Entities;
using AllGoRithmFramework.Repository.Contexts;
using AllGoRithmFramework.Service.DomainServices;
using AllGoRithmFramework.Service.Factories;
using Microsoft.Extensions.Options;
using Response = AGRUsersAPI.DataObjects.Reposnse;

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

        public Response.RegisterUserDto RegisterUser(UserDto userDto)
        {
            if (this.EmailInUse(userDto.Email))
                return new Response.RegisterUserDto().EmailInUse();

            if(!this.ValidEmail(userDto.Email))
                return new Response.RegisterUserDto().InvalidEmail();

            if (this.NameInUse(userDto.UserName))
                return new Response.RegisterUserDto().NameInUse();

            userDto.Password = this.EncryptService.Encrypt(userDto.Password);
            User user = this.UserFactory.Create(userDto);
            this.Insert(user);

            return new Response.RegisterUserDto().RegisterSuccess(user.UserId, user.UserName, user.Email);
        }

        public bool ValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch(FormatException)
            {
                return false;
            }
        }

        public bool UserNameEmailInUse(string userNameEmail)
        {
            if (this.ValidEmail(userNameEmail))
                return this.EmailInUse(userNameEmail) || this.NameInUse(userNameEmail);
            else
                return this.NameInUse(userNameEmail);
        }

        public Response.LoginUserDto Login(string userNameEmail, string password)
        {
            password = this.EncryptService.Encrypt(password);
            User user = this.GetByCredentials(userNameEmail, password);
            
            if(user != null)
                return new Response.LoginUserDto().LoginSuccess(user.UserId, user.UserName, user.Email);
            else
                return new Response.LoginUserDto().LoginFail();
        }
    }
}