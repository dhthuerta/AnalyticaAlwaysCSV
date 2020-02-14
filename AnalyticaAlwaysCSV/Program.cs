using AA.DAL.Data.Repositories.InventoryRepository;
using AA.BL.Services.AA_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using AA.DAL.Data.Factory;
using AA.DAL.Data;
using Microsoft.EntityFrameworkCore;
using AA.DAL.Data.UnitofWork;
using AA.ExternalServices.AzureFileDownload;
using AA.CrossDomain;

namespace AnalyticaAlwaysCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(confBuilder => {
                confBuilder.SetBasePath(Directory.GetCurrentDirectory());
                confBuilder.AddJsonFile("appsettings.json", optional: true);

            })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<IConfiguration>(hostContext.Configuration);
                services.AddSingleton<IInventoryRepo, InventoryRepo>();
                services.AddSingleton<IAA_Service, AA_Service>();
                services.AddSingleton<IAzureFileDownload, AzureFileDownload>();
                services.AddSingleton<IFactory, Factory>();
                services.AddSingleton<IUnitOfWork, UnitOfWork>();
                services.AddHostedService<AA_Manager>();

                var connectionString = hostContext.Configuration[Constants.CONN_STRING_KEY];
                services.AddDbContext<AA_BBDDContext>(
                    opts => opts.UseSqlServer(connectionString, b => b.MigrationsAssembly("AnalyticaAlwaysCSV"))
                     ); ;

            });
    }
}
