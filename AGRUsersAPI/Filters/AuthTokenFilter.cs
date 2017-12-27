using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AGRUsersAPI.Filters
{
    public class AuthTokenFilter : ActionFilterAttribute
    {
        public static IConfiguration Configuration { get; set; }
        
        public AuthTokenFilter()
        {
            var dir = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
            .SetBasePath(dir)
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (Convert.ToBoolean(Configuration["Token:validate"]))
            {
                IHeaderDictionary headers = context.HttpContext.Request.Headers;
                string tokenName = Configuration["Token:name"];
                string tokenValue = Configuration["Token:value"];

                if (!headers[tokenName].Equals(tokenValue))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
