using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using TaskManager.Dtos;
using TaskManager.Entities;
using TaskManager.Middlewares;
using TaskManager.Repository;
using TaskManager.Services;

namespace TaskManager
{
    public class Startup
    {

        private readonly IConfigurationRoot configuration;

        public IConfigurationRoot GetConfiguration()
        {
            return configuration;
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables();

            env.ConfigureNLog("nlog.config");

            configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();            

            services.AddDbContext<TaskManagerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ISeedDataService, SeedDataService>();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info { Title = "Task Manager WebAPI", Version = "v1" });
            });

            services.AddMvc(config=> {

                config.ReturnHttpNotAcceptable = true;
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());

            });

            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (errorFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("GlobExcepLogger");
                            logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                        }

                        await context.Response.WriteAsync("Oops ! Something went wrong. Please contact support.");
                    });
                });
            }
         

            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<TaskEntity, TaskDto>().
                       ForMember(i => i.CreationTime, j => j.MapFrom(k => k.CreationTime.ToShortDateString()))
                       .ForMember(i => i.CompletionTime, j => j.MapFrom(k => k.CompletionTime.ToShortDateString()))
                       .ForMember(i => i.DueDate, j => j.MapFrom(k => k.DueDate.ToShortDateString()))
                       .ReverseMap();
                      
                mapper.CreateMap<TaskEntity, TaskCreateDto>().ReverseMap();
                mapper.CreateMap<TaskEntity, TaskUpdateDto>().ReverseMap();
               


            });

            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager Web Api");
            });        
         
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //app.AddSeedData();

            app.UseMvc();
        }
    }
}
