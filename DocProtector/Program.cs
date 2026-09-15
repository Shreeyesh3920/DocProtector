using DocProtector.Data;
using Microsoft.AspNetCore.Identity;
using DocProtector.Models;
using Microsoft.EntityFrameworkCore;
using DocProtector.Services.Interfaces;
using DocProtector.Services;

namespace DocProtector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Database Connection String: Registered DatabaseContext
            builder.Services.AddDbContext<ApplicationDbContext>((options) => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            //Register Identity Service
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //Register Application Services
            builder.Services.AddScoped<IAuthService, AuthService>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AngularClient", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
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

            app.UseCors("AngularClient");

            app.MapControllers();

            app.Run();
        }
    }
}
