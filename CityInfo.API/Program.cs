using CityInfo.API.DbContexts;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CityInfo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            // Add services to the container.

            // return not acceptable if the requested fomat is not supported
            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

#if DEBUG
            builder.Services.AddTransient<IMailService, LocalMailService>();
#else
            builder.Services.AddTransient<IMailService, CloudMailService>();
#endif
            builder.Services.AddSingleton<CitiesDataStore>();

            builder.Services.AddDbContext<CityInfoContext>(
                dbContextOptions => dbContextOptions.UseSqlite(
                    builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]));

            builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}