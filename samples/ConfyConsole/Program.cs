using System;
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
                    builder.AddUserSecrets<Program>();
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    if(context.HostingEnvironment.IsDevelopment())
                        builder.AddGitSource(source =>
                        {
                            source.Url = "https://gitlab.dell.com/Sharthak_Ghosh/dsa-quote-configurations.git";
                            source.AppName = "dsa-offerworkflow-api-ge4-sit";
                            source.Branch = "master";
                            source.AuthTokenEnvironmentVariableName = context.Configuration["Gitlab:Authtoken"];
                            source.UserNameEnvironmentVariableName = context.Configuration["Gitlab:Username"];
                            source.CloneOptions = (cloneOptions) => {
                                cloneOptions.AlwaysCloneOnStart = true;
                                cloneOptions.CloneSubDir = "confy";
                            };
                        });
                }).Build();

            // This will list GitConfiguration provider as a source for configuration
            var conf = host.Services.GetService<IConfiguration>();

            host.Run();
        }
    }
}
