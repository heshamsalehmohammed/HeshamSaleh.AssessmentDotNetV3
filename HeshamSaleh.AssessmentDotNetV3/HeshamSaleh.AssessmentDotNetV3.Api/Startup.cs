using Flunt.Notifications;
using HeshamSaleh.AssessmentDotNetV3.Api.Middlewares;
using HeshamSaleh.AssessmentDotNetV3.Application;
using HeshamSaleh.AssessmentDotNetV3.Application.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.Application.Results;
using HeshamSaleh.AssessmentDotNetV3.CrossCutting.Assemblies;
using HeshamSaleh.AssessmentDotNetV3.Domain.Entities;
using HeshamSaleh.AssessmentDotNetV3.Domain.Repositories.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.Infrastructure.Context;
using HeshamSaleh.AssessmentDotNetV3.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;

namespace HeshamSaleh.AssessmentDotNetV3.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*            services.AddDbContext<DBContext>(options =>
                        {
                            options.UseSqlServer(
                                @"Server=(localdb)\mssqllocaldb;Database=CourseLibraryDB;Trusted_Connection=True;");
                        });*/
            services.AddDbContext<DBContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("SqlServerConnectionString")));

            services.AddControllers()
                    .AddNewtonsoftJson()
                    .AddXmlDataContractSerializerFormatters()
                    .ConfigureApiBehaviorOptions(setupAction =>
                    {
                        setupAction.InvalidModelStateResponseFactory = context =>
                        {
                            // create a problem details object
                            var problemDetailsFactory = context.HttpContext.RequestServices
                                .GetRequiredService<ProblemDetailsFactory>();
                            var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                                    context.HttpContext,
                                    context.ModelState);

                            // add additional info not added by default
                            // problemDetails.Detail = "See the errors field for details.";
                            // problemDetails.Instance = context.HttpContext.Request.Path;

                            // find out which status code to use
                            /*                            var actionExecutingContext =
                                                              context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;*/

                            // if there are modelstate errors & all keys were correctly
                            // found/parsed we're dealing with validation errors
                            //
                            // if the context couldn't be cast to an ActionExecutingContext
                            // because it's a ControllerContext, we're dealing with an issue 
                            // that happened after the initial input was correctly parsed.  
                            // This happens, for example, when manually validating an object inside
                            // of a controller action.  That means that by then all keys
                            // WERE correctly found and parsed.  In that case, we're
                            // thus also dealing with a validation error.
                            /*                            if (context.ModelState.ErrorCount > 0 &&
                                                            (context is ControllerContext ||
                                                             actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                                                        {
                                                            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                                                            problemDetails.Title = "One or more validation errors occurred.";

                                                            return new UnprocessableEntityObjectResult(problemDetails)
                                                            {
                                                                ContentTypes = { "application/problem+json" }
                                                            };
                                                        }*/

                            // if one of the keys wasn't correctly found / couldn't be parsed
                            // we're dealing with null/unparsable input
                            // problemDetails.Status = StatusCodes.Status400BadRequest;
                            // problemDetails.Title = "One or more errors on input occurred.";

                            return new BadRequestObjectResult(Result.Error(problemDetails.Errors))
                            {
                                ContentTypes = { "application/problem+json" }
                            };
                        };
                    });

            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());

            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache));

            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddSwaggerGen();
        }

        internal static IActionResult ProblemDetailsInvalidModelStateResponse(
                 ProblemDetailsFactory problemDetailsFactory, ActionContext context)
        {
            var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);
            ObjectResult result;
            if (problemDetails.Status == 400)
            {
                // For compatibility with 2.x, continue producing BadRequestObjectResult instances if the status code is 400.
                result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                result = new ObjectResult(problemDetails);
            }
            result.ContentTypes.Add("application/problem+json");
            result.ContentTypes.Add("application/problem+xml");

            return result;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "HeshamSaleh.AssessmentDotNetV3.Api");
                opt.RoutePrefix = "";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new JsonExceptionMiddleware().Invoke
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
