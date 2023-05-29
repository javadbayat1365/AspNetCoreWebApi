using Data;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Configuration
{
    public static class AddIdentityConfigurationExtentions
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User,Role>(options => { 

                //Password Setting
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;

                //SignIn Settings => work With Cookie
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = true;

                //Lockout Settings => work With Cookie
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;
                options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(5);

            }).AddEntityFrameworkStores<AppDBContext>()
            .AddDefaultTokenProviders();
        }
    }
}
