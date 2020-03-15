using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Gateways {
    public class Program {
        public static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<Persistance.AppDbContext>()) {
                context.Database.EnsureCreated();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
