using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotificationsService.Models;
using System.Text;

namespace NotificationsService.Extensions
{
    public static class AuthExtensions
    {
        public static void AddApiAuthentication(
            this IServiceCollection services,
            IOptions<JWTOptions> options)
        {
            var value = options.Value;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                { //будет осуществляться проверка, есть ли в headers токен
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(value.SecurityKey))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["access"];

                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
