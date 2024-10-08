using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.APIs.Controllers;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var WebApplicationBuilder = WebApplication.CreateBuilder(args);


            #region Configure Services

            // Add services to the container.

            WebApplicationBuilder.Services
                .AddControllers()
                .AddApplicationPart(typeof(Controllers.Controllers.AssemblyInformation).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            WebApplicationBuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();


            //WebApplicationBuilder.Services.AddScoped(typeof(IHttpContextAccessor) , typeof(HttpContextAccessor));
            WebApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService) , typeof(LoggedInUserService));
            WebApplicationBuilder.Services.AddApplicationServices();


            WebApplicationBuilder.Services.AddPersistenceServices(WebApplicationBuilder.Configuration);


            #endregion

            var app = WebApplicationBuilder.Build(); 

            #region Databases Initialization

            await app.InitializeStoreContextAsync();

            #endregion


            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            #endregion


            app.Run();
        }
    }
}
