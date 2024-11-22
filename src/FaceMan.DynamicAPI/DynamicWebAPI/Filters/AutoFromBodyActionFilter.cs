using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FaceMan.DynamicWebAPI.Filters
{
    public class AutoFromBodyActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.ContentType != null && context.HttpContext.Request.ContentType.Contains("application/json"))
            {
                var body = await new StreamReader(context.HttpContext.Request.Body).ReadToEndAsync();
                if (!string.IsNullOrEmpty(body))
                {
                    object obj = JsonSerializer.Deserialize(body, context.ActionDescriptor.Parameters[0].ParameterType);
                    context.ActionArguments[context.ActionDescriptor.Parameters[0].Name] = obj;
                }
            }

            await next();
        }
    }
}
