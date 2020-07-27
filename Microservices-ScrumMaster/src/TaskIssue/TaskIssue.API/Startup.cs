using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TaskIssue.API.Data;
using TaskIssue.API.Data.Interfaces;
using TaskIssue.API.Repositories;
using TaskIssue.API.Repositories.interfaces;
using TaskIssue.API.Settings;

namespace TaskIssue.API
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

            #region Configuration Dependencies

            services.Configure<TaskIssueDatabaseSettings>(Configuration.GetSection(nameof(TaskIssueDatabaseSettings)));

            services.AddSingleton<ITaskIssueDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TaskIssueDatabaseSettings>>().Value);

            #endregion

            #region Project Dependencies

            services.AddTransient<ITaskIssueContext, TaskIssueContext>();
            services.AddTransient<IIssueRepository, IssueRepository>();

            #endregion

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskIssue API", Version = "v1" });
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskIssue API V1");
            });
        }
    }
}
