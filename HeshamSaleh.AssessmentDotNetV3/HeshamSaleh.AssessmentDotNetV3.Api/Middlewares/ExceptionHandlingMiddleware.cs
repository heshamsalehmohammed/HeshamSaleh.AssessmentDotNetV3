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
    public class ExceptionHandlingMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await Handle500LevelExceptionAsync(context, exception);
            }
        }

        public async Task Handle500LevelExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(Result.Error(exception.Message, exception.StackTrace));
            await context.Response.WriteAsync(result);
        }
    }
}
