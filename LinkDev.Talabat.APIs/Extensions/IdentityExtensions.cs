using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

             
            services.AddIdentity<ApplicationUser, IdentityRole>((identityOptions) =>
            {
                ///identityOptions.SignIn.RequireConfirmedAccount = true;
                ///identityOptions.SignIn.RequireConfirmedEmail = true;
                ///identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

                /// identityOptions.Password.RequireNonAlphanumeric = true;  // #@$%
                /// identityOptions.Password.RequiredUniqueChars = 2;
                /// identityOptions.Password.RequiredLength = 6;
                /// identityOptions.Password.RequireDigit = true;
                /// identityOptions.Password.RequireLowercase = true;
                /// identityOptions.Password.RequireUppercase = true;


                identityOptions.User.RequireUniqueEmail = true;
                //identityOptions.User.AllowedUserNameCharacters = "";


                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);


                //identityOptions.Stores
                //identityOptions.Tokens
                //identityOptions.ClaimsIdentity

            })
                .AddEntityFrameworkStores<StoreIdentityContext>(); ;


            services.AddAuthentication((authenciationOptions) =>
            {
                authenciationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authenciationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer((options)=>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = configuration["JWTSettings:Audience"],
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]!)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IAuthService>();
            });

            return services;
        }


    }
}
