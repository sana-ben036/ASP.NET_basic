using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ManageStudent1.Models;
using ManageStudent1.Models.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace ManageStudent1
{
    public class Startup
    {
        readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("StudentDbConnect"))); // connectiondb
            services.AddIdentity<AppUser, IdentityRole>(options => //AppUser was IdentityUser
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }
                
                ).AddEntityFrameworkStores<AppDbContext>();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;  // for accept ouwn routing
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "464133127211-av4ifv26jeg03svq702oo9q7ljovn19q.apps.googleusercontent.com";
                    options.ClientSecret = "WWyew2jNIbE6Ju_fjHhYzjJc";

                })
                .AddFacebook(options =>
                {
                    options.AppId = "122897433076924";
                    options.AppSecret = "17c9b2f1b0da5cb0e1df2b3ef9417b44";

                })
                .AddTwitter(options =>
                {
                    options.ConsumerKey = "122897433076924";
                    options.ConsumerSecret= "17c9b2f1b0da5cb0e1df2b3ef9417b44";

                })
                ;


            services.AddTransient<ICompanyRepository<Student>, SqlStudentRepository>(); // changemement addSingleton StudentRepository
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");// se declancher en mode production
            }


            app.UseFileServer();
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Student}/{action=Index}/{id ?}");
            });

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {

            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
