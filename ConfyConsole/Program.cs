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

            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddGitSource(source =>
                    {
                        source.Url = "https://github.com/coderookie1994/rxjs-cache.git";
                        source.AppName = "test";
                        source.Branch = "master";
                    });
                }).Build();

            var conf = host.Services.GetService<IConfiguration>();

            host.Run();
        }
    }
}
