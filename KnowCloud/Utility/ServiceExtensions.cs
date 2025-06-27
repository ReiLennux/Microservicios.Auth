using KnowCloud.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;

namespace KnowCloud.Utility
{
    public static class ServiceExtensions
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddControllers ( options =>
               {
                   options.Filters.Add<CustomExceptionFilterAttribute>();
               }
            );

            services.AddHttpContextAccessor();

            services.AddHttpClient<IAuthService, AuthService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddTransient<IDataCloudAzure, DataCloudAzure>();
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<ITokenProvider, TokenProvider>();

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/Denied";
                    options.SlidingExpiration = true;
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("RoleAdmin"));
            });
        }
    }
}
