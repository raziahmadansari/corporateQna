using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PetaPoco;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        private readonly Container Container = new Container();
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", config =>
                {
                    config.Authority = "https://localhost:44303/"; // Auth server (Identity server)
                    config.Audience = "api";
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddCors(config =>
            {
                config.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            services.AddSimpleInjector(Container, options =>
            {
                options.AddAspNetCore()
                       .AddControllerActivation();
            });

            //Container.Register<Database>(() => new Database(Configuration.GetConnectionString("CorporateQna"), "System.Data.SqlClient"), Lifestyle.Scoped);
            Container.Register<IUserService, UserService>(Lifestyle.Scoped);
            Container.Register<ICategoryService, CategoryService>(Lifestyle.Scoped);
            Container.Register<IQuestionService, QuestionService>(Lifestyle.Scoped);
            Container.Register<ISolutionService, SolutionService>(Lifestyle.Scoped);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
