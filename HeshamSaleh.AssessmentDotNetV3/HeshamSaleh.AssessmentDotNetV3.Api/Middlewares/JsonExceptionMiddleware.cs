using HeshamSaleh.AssessmentDotNetV3.Application.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HeshamSaleh.AssessmentDotNetV3.Api.Middlewares
{
    public class JsonExceptionMiddleware
    {
        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
            if (null != exceptionObject)
            {
                var result = JsonConvert.SerializeObject(Result.Error(exceptionObject.Error.Message, exceptionObject.Error.StackTrace));
                await context.Response.WriteAsync(result);
            }
        }
    }
}
