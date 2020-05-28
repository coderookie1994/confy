using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApiSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder => builder.AddUserSecrets<Program>())
                .ConfigureAppConfiguration((context, builder) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                        builder.AddGitSource(source =>
                        {
                            source.Url = "https://gitlab.dell.com/Sharthak_Ghosh/dsa-quote-configurations.git";
                            source.AppName = "dsa-offerworkflow-api-ge4-sit";
                            source.Branch = "master";
                            source.AuthTokenEnvironmentVariableName = context.Configuration["Gitlab:Authtoken"];
                            source.UserNameEnvironmentVariableName = context.Configuration["Gitlab:Username"];
                            source.AlwaysCloneOnStart = true;
                        });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
