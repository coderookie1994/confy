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
                            source.Url = context.Configuration["Github:Url"];
                            source.AppName = context.Configuration["Github:AppName"];
                            source.Branch = "master";
                            source.AuthTokenEnvironmentVariableName = context.Configuration["Gitlab:Authtoken"];
                            source.UserNameEnvironmentVariableName = context.Configuration["Gitlab:Username"];
                            source.CloneOptions = options =>
                            {
                                options.CloneSubDir = "sample";
                                options.AlwaysCloneOnStart = true;
                            };
                        });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
