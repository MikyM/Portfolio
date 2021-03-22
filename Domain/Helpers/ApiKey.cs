using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
 
namespace SecuringWebApiUsingApiKey.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey)) {
                context.Result = new UnauthorizedObjectResult(new { message = "Api Key was not provided" });
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = appSettings.GetValue<string>("AppSettings:ApiKey");

            if (!apiKey.Equals(extractedApiKey)) {
                context.Result = new UnauthorizedObjectResult(new { message = "Api Key is not valid" });
                return;
            }

            await next();
        }
    }
}
