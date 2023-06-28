using Microsoft.EntityFrameworkCore;
using Practical18.Data;
using Practical18.Repository;
using System;
using Sentry;

namespace Practical18
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
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IstudentRepository, StudentRepository>();

            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            SentrySdk.Init("https://e205289547e14e2d8796a26046554cba@o4505436274819072.ingest.sentry.io/4505436294348800");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
  

            app.MapControllers();

            app.Run();
        }
    }
}