using Core.Interfaces.Auth;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace API.Extensions
{
    public static class JwtServicesExtensions
    {
        public static IServiceCollection AddJwtServices(this IServiceCollection services,IConfiguration config)
        {
            // Get JWT Token Settings from appsettings.json file
            JWTContainerModel settings = GetJwtSettings(config);
            //// Create singleton of JwtSettings the problem with singleton is the thread safe subject
            services.AddSingleton<JWTContainerModel>(settings);

            //when we request IAuthService in the controller constractor the api will do new JWTService(settings.SecretKey)
            
            services.AddScoped<IAuthService>(sp => new JWTService(settings.SecretKey));
            //Set Authentication אימות כניסה/אימות טוקן
            ////////////////////////////////////////////////
            //services.AddAuthentication(options =>
            //{
            //    //When the request arived search in the request header the Authentication setting
            //    //it sepose to look like --> Authentication JwtBearer:gfgsdfadrr76612yfgfgas (Value)
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme = "JwtBearer";
            //    options.DefaultChallengeScheme = "JwtBearer";
            //})
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"])),

            //        ValidIssuer = config["JWT:Issuer"],
            //        ValidateIssuer = true,
            //        ValidateAudience = false
            //    };
            //});
            .AddJwtBearer(options =>
            {
                //Here we define how to check the token value the we set in the header
                options.TokenValidationParameters =
              new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey)),
                  ValidateIssuer = true,
                  ValidIssuer = settings.Issuer,

                  ValidateAudience = true,
                  ValidAudience = settings.Audience,

                  ValidateLifetime = true,
                  ClockSkew = TimeSpan.FromMinutes(
                         settings.MinutesToExpiration)
              };
            });
            return services;
        }

        private static JWTContainerModel GetJwtSettings(IConfiguration _config)
        {
            JWTContainerModel settings = new JWTContainerModel();

            settings.SecretKey = _config["JWT:Key"];
            settings.Audience = _config["JWT:Audience"];
            settings.Issuer = _config["JWT:Issuer"];
            settings.MinutesToExpiration = Convert.ToInt32(_config["JWT:MinutesToExpiration"]);
            return settings;
        }
    }
}
