using CoolParking.WebAPI;
using WebAPI.Middlewares;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            ConfigurePipeline(app, builder.Configuration);

            app.Run();
        }


        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen();
            services.AddControllers();
            services.RegisterRecipeBookServices(configuration);
        }

        private static void ConfigurePipeline(WebApplication app, IConfiguration configuration)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();

            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            // Global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.MapControllers();
        }
    }
}