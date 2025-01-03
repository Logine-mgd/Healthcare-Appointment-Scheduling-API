
using MedConnect_API.Mapper;
using MedConnect_API.Models;
using MedConnect_API.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace MedConnect_API
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
            builder.Services.AddDbContext<HealthCareContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped<unitofwork>();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<HealthCareContext>();
            builder.Services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(
    op =>
    {
        op.SaveToken = true;
        string key = "Secret key Logine Magdy Secret Key";
        var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        op.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = secertkey,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Health Care Appointment Scheduling",
                    Version = "v1",
                    Description = "The Health Care Appointment Scheduling API provides a backend for a healthcare appointment scheduling platform, enabling patients to book appointments with healthcare providers, manage their medical history.\n It includes features for patient and provider management, appointment scheduling, and secure access to patient records.\r\n"
                }
                );
                c.EnableAnnotations();
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
