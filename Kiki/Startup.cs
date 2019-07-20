using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiki.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kiki
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var connection = "Data Source=kiki.db";
            services.AddDbContext<KikiContext>(options => options.UseSqlite(connection));
            services.AddIdentity<KikiUser, KikiRole>(i =>
                {
                    //warning: placeholder development settings, change to something sane later
                    i.User.RequireUniqueEmail = true;
                    i.Password.RequireDigit = false;
                    i.Password.RequiredLength = 1;
                    i.Password.RequireLowercase = false;
                    i.Password.RequireUppercase = false;
                    i.Password.RequiredUniqueChars = 1;
                })
                .AddEntityFrameworkStores<KikiContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<KikiUser, KikiRole, KikiContext, Guid>>()
                .AddRoleStore<RoleStore<KikiRole, KikiContext, Guid>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}