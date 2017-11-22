using System;
using System.Net;
using AGRUsersAPI.Configuration;
using AGRUsersAPI.Services;
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

        [HttpPost]
        public Response.RegisterUserDto Post([FromBody]UserDto user)
        {
            Response.RegisterUserDto registerUserResponse = new Response.RegisterUserDto();

            try
            {
                registerUserResponse = this.UserService.RegisterUser(user);

                if(registerUserResponse.Success)
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
            catch(Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                registerUserResponse.ResponseMessage = ex.Message;
                return registerUserResponse;
            }
        }
    }
}