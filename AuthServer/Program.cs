using AuthServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var user = new ApplicationUser
                {
                    UserName = "test",
                    Name = "Test Me",
                    Designation = "Senior Manager",
                    Team = "Dev",
                    Category = "Product Development",
                    Location = "Hyderabad"
                };

                userManager.CreateAsync(user, "password").GetAwaiter().GetResult();

                var user2 = new ApplicationUser
                {
                    UserName = "bob",
                    Name = "Bob Trssel",
                    Designation = "Senior Manager",
                    Team = "Dev",
                    Category = "Product Development",
                    Location = "Hyderabad"
                };

                userManager.CreateAsync(user2, "password").GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
