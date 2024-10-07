using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var storeContextIntializer = services.GetRequiredService<IStoreContextIntializer>();
            // Ask Runtime Eny For An Object from "StoreContext" Service Explicitly.

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {

                await storeContextIntializer.InitializeAsync();
                await storeContextIntializer.SeedAsync();

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during applying the migrations or The data seeding. ");

            }

            return app;
        }
    }
}
