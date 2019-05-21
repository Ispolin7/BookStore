using BookStore.DataAccess;
using BookStore.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Common.Extensions
{
    public static class IdentityServicesExtension
    {
        /// <summary>
        /// Service collection extension for adding identity params
        /// </summary>
        /// <param name="services">service collection</param>
        /// <returns>updated service collection</returns>
        public static IServiceCollection AddIdentityParams(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 7;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<BookStoreContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        /// <summary>
        /// Service collection extension for adding jwt authentication
        /// </summary>
        /// <param name="services">service collection</param>
        /// <returns>updated service collection</returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            // Add Authentication
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new JWTValidationParameters();
                    });

            return services;
        }
    }
}
