using Login_Register_Api.Context;
using Login_Register_Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace Login_Register_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //connect to database
            builder.Services.AddDbContext<LoginContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defualtconnection"));
            });
            //add token services 
            builder.Services.AddScoped<Token>();

            //add cors policy 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", corsPolicyOptions =>
                {
                    corsPolicyOptions.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors("MyPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}