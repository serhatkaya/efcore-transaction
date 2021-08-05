using efcore_transaction.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace efcore_transaction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

            System.Console.WriteLine(configuration.GetValue<string>("ConnectionStrings:DbConnection"));

            return Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<Worker>()
                   .AddDbContext<WorkerContext>(options => options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:DbConnection")), ServiceLifetime.Singleton);
               });
        }
    }
}
