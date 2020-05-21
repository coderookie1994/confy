using System;
using System.Reflection;
using Confy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConfyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddUserSecrets<Program>(false);
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    if(context.HostingEnvironment.IsDevelopment())
                        builder.AddGitSource(source =>
                        {
                            source.Url = "https://github.com/coderookie1994/rxjs-cache.git";
                            source.AppName = "test";
                            source.Branch = "master";
                            source.AuthTokenEnvironmentVariableName = context.Configuration["Gitlab:Authtoken"];
                            source.UserNameEnvironmentVariableName = context.Configuration["Gitlab:Username"];
                            source.AlwaysCloneOnStart = true;
                        });
                }).Build();

            var conf = host.Services.GetService<IConfiguration>();

            host.Run();
        }
    }
}
