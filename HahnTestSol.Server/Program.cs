using HahnTestSol.Server.Services;
using HahnTestSol.Server.Services.Legacy;
using HahnTestSol.Server.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HahnTestSol.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // JWT configuration starts here
            var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });
            // JWT configuration ends here

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register AuthService
            builder.Services.AddHttpClient<LegacyAuthService>();

            // Register AuthenticatedHttpClientHandler with a factory method
            builder.Services.AddTransient<AuthenticatedHttpClientHandler>(provider =>
            {
                var authService = provider.GetRequiredService<LegacyAuthService>();
                var username = "your-username"; // Replace with actual username
                return new AuthenticatedHttpClientHandler(authService, username);
            });

            // Register HttpClient with AuthenticatedHttpClientHandler
            builder.Services.AddHttpClient("AuthenticatedClient")
                .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            // Register CargoService
            builder.Services.AddTransient<CargoService>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // Add authentication middleware
            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
