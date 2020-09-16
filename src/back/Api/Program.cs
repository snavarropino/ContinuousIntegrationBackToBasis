using Api.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Api.Model;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                //.MigrateDbContext((context, services) =>
                //{
                //    context.Database.Migrate();

                //    //https://github.com/dotnet/AspNetCore.Docs/issues/16452
                //    if (!context.Heroes.Any())
                //    {
                //        context.Heroes.AddRange(GetInitialHeroes());
                //        context.SaveChanges();
                //    }
                //})
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
