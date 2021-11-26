using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Server.Service;
using Server.DAO;
using Newtonsoft.Json;
using Newtonsoft;
namespace TodoApi
{
    public class Startup
    {
        private string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // services.AddCors(); 原本的配置

            // 新的配置，不限制跨域请求
            services.AddCors(options =>
                        options.AddPolicy(MyAllowSpecificOrigins,
                         builder =>
                         {
                             builder.WithOrigins("http://localhost:8080",
                                                 "*").AllowAnyHeader()
                                                  .AllowAnyMethod(); 
                         }));
            services.AddDbContextPool<ServerContext>(
                options => options.UseMySql(
                    Configuration.GetConnectionString("db"),
                    options => options.ServerVersion(new Version(5, 7, 30), ServerType.MySql))
               );
            services.AddScoped<IService, UserService>();
            services.AddScoped<IService, AdminService>();
            services.AddScoped<IService, PostService>();
            services.AddScoped<IService, ReplyService>();
            services.AddScoped<IService, ReportService>();
            services.AddScoped<IService, TokenService>();

            services.AddSignalR().AddNewtonsoftJsonProtocol(options =>
            {
                options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();
   

            /*
            app.UseCors(builder =>
            {
                builder.WithOrigins(GlobalContext.SystemConfig.AllowCorsSite.Split(',')).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
            */

            
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
