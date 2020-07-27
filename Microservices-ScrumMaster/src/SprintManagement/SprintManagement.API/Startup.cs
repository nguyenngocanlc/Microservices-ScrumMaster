using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMQ;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using SprintManagement.API.Extentions;
using SprintManagement.API.RabbitMQ;
using SprintManagement.Application.Handlers;
using SprintManagement.Application.PipelineBehaviours;
using SprintManagement.Core.Repositories;
using SprintManagement.Core.Repositories.Base;
using SprintManagement.Infrastructure.Data;
using SprintManagement.Infrastructure.Repository;
using SprintManagement.Infrastructure.Repository.Base;

namespace SprintManagement.API
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
            services.AddControllers();

            services.AddDbContext<SprintContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("SprintConnection")));

            #region Project Dependencies
            // Add Infrastructure Layer
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ISprintRepository), typeof(SprintRepository));
            services.AddScoped(typeof(ISprintDetailRepository), typeof(SprintDetailRepository));
            services.AddTransient<ISprintRepository, SprintRepository>(); // we made transient this in order to resolve in mediatR when consuming Rabbit
            services.AddTransient<ISprintDetailRepository, SprintDetailRepository>(); // we made transient this in order to resolve in mediatR when consuming Rabbit

            // Add AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Add MediatR
            services.AddMediatR(typeof(UpdateIssueInSprintHandle).GetTypeInfo().Assembly);

            //Domain Level Validation
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            #endregion

            #region RabbitMQ Dependencies

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]
                };

                if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
                {
                    factory.UserName = Configuration["EventBus:UserName"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                {
                    factory.Password = Configuration["EventBus:Password"];
                }

                return new RabbitMQConnection(factory);
            });

            services.AddSingleton<EventBusRabbitMQConsumer>();

            #endregion

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SprintManagement API", Version = "v1" });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Initilize Rabbit Listener in ApplicationBuilderExtentions
            app.UseRabbitListener();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SprintManagement API V1");
            });
        }
    }
}
