using HeshamSaleh.AssessmentDotNetV3.Application;
using HeshamSaleh.AssessmentDotNetV3.Application.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.CrossCutting.Assemblies;
using HeshamSaleh.AssessmentDotNetV3.Domain.Repositories.Interfaces;
using HeshamSaleh.AssessmentDotNetV3.Infrastructure.Context;
using HeshamSaleh.AssessmentDotNetV3.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
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
using Newtonsoft.Json.Serialization;
using System.Linq;

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
                    .AddXmlDataContractSerializerFormatters();

            services.AddAutoMapper(AssemblyUtil.GetCurrentAssemblies());

            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache));

            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddSwaggerGen();
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
