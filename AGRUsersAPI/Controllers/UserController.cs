using AGRUsersAPI.Filters;
using AGRUsersAPI.Services.DomainServices;
using AllGoRithmFramework.Domain.Configurations;
using AllGoRithmFramework.Domain.DataObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using Response = AGRUsersAPI.DataObjects.Reposnse;

namespace AGRUsersAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [AuthTokenFilter]
    public class UserController : Controller
    {
        private UserService UserService { get; set; }

        public UserController(IOptions<EncryptConfiguration> encryptConfiguration, IOptions<DbContextConfiguration> dbContextConfiguration)
        {
            this.UserService = new UserService(encryptConfiguration, dbContextConfiguration);
        }

        [HttpGet]
        [Route("ExistentUserNameEmail")]
        public bool ExistentUserNameEmail(string userNameEmail)
        {
            try
            {
                return this.UserService.UserNameEmailInUse(userNameEmail);
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return false;
            }
        }

        [HttpPost]
        [Route("Register")]
        public Response.RegisterUserDto Register([FromBody]UserDto user)
        {
            Response.RegisterUserDto registerUserResponse = new Response.RegisterUserDto();

            try
            {
                registerUserResponse = this.UserService.RegisterUser(user);

                if (registerUserResponse.Success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
                    return registerUserResponse;
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return registerUserResponse;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                registerUserResponse.ResponseMessage = ex.Message;
                return registerUserResponse;
            }
        }

        [HttpPost]
        [Route("Login")]
        public Response.LoginUserDto Login([FromHeader]string userNameEmail, [FromHeader]string password)
        {
            Response.LoginUserDto loginUserResponse = new Response.LoginUserDto();

            try
            {
                loginUserResponse = this.UserService.Login(userNameEmail, password);

                if(loginUserResponse.Success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    return loginUserResponse;
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return loginUserResponse;
                }
            }
            catch(Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                loginUserResponse.ResponseMessage = ex.Message;
                return loginUserResponse;
            }
        }

        [HttpPost]
        [Route("Logout")]
        public Response.LogoutUserDto Logout(int userId)
        {
            Response.LogoutUserDto logOutUserResponse = new Response.LogoutUserDto();

            try
            {
                logOutUserResponse = this.UserService.Logout(userId);
                
                if(logOutUserResponse.Success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    return logOutUserResponse;
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return logOutUserResponse;
                }
            }
            catch(Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                logOutUserResponse.ResponseMessage = ex.Message;
                return logOutUserResponse;
            }
        }
    }
}