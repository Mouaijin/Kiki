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
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog.Extensions.Logging;
using Serilog;
using Serilog.AspNetCore;
using TagLib;
using SerilogLoggerFactory = Serilog.Extensions.Logging.SerilogLoggerFactory;

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
            services.AddControllers();
            services.AddDbContext<KikiContext>(options =>
                                               {
                                                   options.UseLoggerFactory(new SerilogLoggerFactory()).UseSqlite("Data Source=kiki.db");
                                               });
            services.AddIdentity<KikiUser, KikiRole>(
                                               i =>
                                               {
                                                   //warning: placeholder development settings, change to something sane later
                                                   i.User.RequireUniqueEmail         = true;
                                                   i.Password.RequireDigit           = false;
                                                   i.Password.RequiredLength         = 1;
                                                   i.Password.RequireLowercase       = false;
                                                   i.Password.RequireUppercase       = false;
                                                   i.Password.RequiredUniqueChars    = 1;
                                                   i.Password.RequireNonAlphanumeric = false;
                                               }
                                              )
                    .AddRoles<KikiRole>()
                    .AddEntityFrameworkStores<KikiContext>()
                    .AddDefaultTokenProviders()
                    .AddUserStore<UserStore<KikiUser, KikiRole, KikiContext, Guid>>()
                    .AddRoleStore<RoleStore<KikiRole, KikiContext, Guid>>();
            services.AddHttpContextAccessor();
            // Identity services
            services.TryAddScoped<IUserValidator<KikiUser>, UserValidator<KikiUser>>();
            services.TryAddScoped<IPasswordValidator<KikiUser>, PasswordValidator<KikiUser>>();
            services.TryAddScoped<IPasswordHasher<KikiUser>, PasswordHasher<KikiUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<KikiRole>, RoleValidator<KikiRole>>();
            // No interface for the error describer so we can add errors without rev'ing the interface
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<KikiUser>>();
            services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<KikiUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<KikiUser>, UserClaimsPrincipalFactory<KikiUser, KikiRole>>();
            services.TryAddScoped<UserManager<KikiUser>>();
            services.TryAddScoped<SignInManager<KikiUser>>();
            services.TryAddScoped<RoleManager<KikiRole>>();
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
               .AddAuthentication(options =>
                                  {
                                      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                      options.DefaultScheme             = JwtBearerDefaults.AuthenticationScheme;
                                      options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
                                  })
               .AddJwtBearer(cfg =>
                             {
                                 cfg.RequireHttpsMetadata = false;
                                 cfg.SaveToken            = true;
                                 cfg.TokenValidationParameters = new TokenValidationParameters
                                                                 {
                                                                     ValidIssuer      = Configuration["JwtIssuer"],
                                                                     ValidAudience    = Configuration["JwtIssuer"],
                                                                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                                                                     ClockSkew        = TimeSpan.Zero // remove delay of token when expire
                                                                 };
                             });
        }
        
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, KikiContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            if (!System.IO.File.Exists("kiki.db"))
            {
                System.IO.File.Create("kiki.db");
            }
            // ===== Create tables ======
            dbContext.Database.EnsureCreated();
        }
    }
}