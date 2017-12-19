using AGRUsersAPI.Domain.Entities;
using AllGoRithmFramework.Domain.Configurations;
using AllGoRithmFramework.Domain.DataObjects;
using AllGoRithmFramework.Domain.Entities;
using AllGoRithmFramework.Repository.Contexts;
using AllGoRithmFramework.Service.DomainServices;
using AllGoRithmFramework.Service.Factories;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using Response = AGRUsersAPI.DataObjects.Reposnse;

namespace AGRUsersAPI.Services.DomainServices
{
    public class UserService : AllGoRithmFramework.Service.DomainServices.UserService
    {
        protected UserFactory UserFactory { get; set; }

        protected EncryptService EncryptService { get; set; }

        protected UserLogService UserLogService { get; set; }

        public UserService(IOptions<EncryptConfiguration> encryptConfiguration, IOptions<DbContextConfiguration> dbContextConfiguration)
            : base(new BaseContext<User>(dbContextConfiguration.Value.ConnectionString))
        {
            string iv = encryptConfiguration.Value.Iv;
            string key = encryptConfiguration.Value.Key;

            this.UserFactory = new UserFactory();
            this.EncryptService = new EncryptService(key, iv);
            this.UserLogService = new UserLogService(new BaseContext<UserLog>(dbContextConfiguration.Value.ConnectionString));
        }

        public Response.RegisterUserDto RegisterUser(UserDto userDto)
        {
            if (this.EmailInUse(userDto.Email))
                return new Response.RegisterUserDto().EmailInUse();

            if (!this.ValidEmail(userDto.Email))
                return new Response.RegisterUserDto().InvalidEmail();

            if (this.NameInUse(userDto.UserName))
                return new Response.RegisterUserDto().NameInUse();

            userDto.Password = this.EncryptService.Encrypt(userDto.Password);
            User user = this.UserFactory.Create(userDto);
            this.Insert(user);

            return new Response.RegisterUserDto().RegisterSuccess(user.UserId, user.UserName, user.Email);
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
            Response.LoginUserDto result;

            if (user != null)
                result = new Response.LoginUserDto().LoginSuccess(user.UserId, user.UserName, user.Email);
            else
                result = new Response.LoginUserDto().LoginFail();

            if(result.Success)
                this.UserLogService.LogLogin(result.UserId);

            return result;
        }

        public Response.LogoutUserDto Logout(int userId)
        {
            Response.LogoutUserDto result;

            if(this.UserLogService.LogLogout(userId))
            {
                User user = this.GetById(userId);
                result = new Response.LogoutUserDto().SuccessLogout(user.UserId, user.UserName, user.Email);
            }
            else
            {
                result = new Response.LogoutUserDto().LogoutFail();
            }
            
            return result;
        }

        private bool ValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}