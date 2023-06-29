using Microsoft.EntityFrameworkCore;
using Practical18.Data;
using Practical18.Repository;
using System;
using Sentry;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Practical18.Services.Health;
using HealthChecks.UI.Client;

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
            //add api health check
            builder.Services.AddHealthChecks()
                .AddCheck<ApiHealthCheck>("StudentAPICheck",tags: new string[] {"StudentAPI"}); 
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            SentrySdk.Init("https://e205289547e14e2d8796a26046554cba@o4505436274819072.ingest.sentry.io/4505436294348800");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                // Map health check endpoint
                endpoints.MapHealthChecks("/health",new HealthCheckOptions()
                {
                    Predicate = _=> true,
                    ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapControllers();
            });
            //app.MapControllers();

            app.Run();
        }
    }
}