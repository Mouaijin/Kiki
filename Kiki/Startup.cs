using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiki.Models;
using Kiki.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;

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
            // configure jwt authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
               .AddAuthentication(options =>
                                  {
                                      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                  })
               .AddJwtBearer(cfg =>
                             {
                                 cfg.RequireHttpsMetadata = false;
                                 cfg.SaveToken = true;
                                 cfg.TokenValidationParameters = new TokenValidationParameters
                                                                 {
                                                                     ValidIssuer = Configuration["JwtIssuer"],
                                                                     ValidAudience = Configuration["JwtIssuer"],
                                                                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                                                                     ClockSkew = TimeSpan.Zero // remove delay of token when expire
                                                                 };
                             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, KikiContext dbContext)
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
            // global cors policy
            app.UseCors(x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseMvc();

            // ===== Create tables ======
            dbContext.Database.EnsureCreated();
        }
    }
}