using AGRUsersAPI.Configuration;
using AGRUsersAPI.Services;
using AllGoRithmFramework.Domain.DataObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        public void Post([FromBody]UserDto user)
        {
            this.UserService.RegisterUser(user);
        }
    }
}