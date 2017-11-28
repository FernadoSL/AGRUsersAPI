using System;
using System.Net;
using AGRUsersAPI.Configuration;
using AGRUsersAPI.Services;
using AGRUsersAPI.Services.DomainServices;
using AllGoRithmFramework.Domain.DataObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Response = AGRUsersAPI.DataObjects.Reposnse;

namespace AGRUsersAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        protected UserService UserService { get; set; }

        public UserController(IOptions<EncryptConfiguration> encryptConfiguration, IOptions<DbContextConfiguration> dbContextConfiguration)
        {
            this.UserService = new UserService(encryptConfiguration, dbContextConfiguration);
        }

        [HttpGet]
        [Route("ExistentUserNameEmail")]
        public bool Get(string userNameEmail)
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
        public Response.RegisterUserDto Post([FromBody]UserDto user)
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
        public Response.LoginUserDto Post([FromHeader]string userNameEmail, [FromHeader]string password)
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
        public void Post(int userId)
        {
            this.UserService.Logout(userId);
        }
    }
}