using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Hosting;

namespace test_wa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseHttpSys(options =>
                        {
                            options.Authentication.Schemes = AuthenticationSchemes.Negotiate;
                            options.Authentication.AllowAnonymous = false;
                        });

                });
    }
}
