using System;
using System.Runtime.InteropServices;
using Confy.Git;
using Confy.Infrastructure.models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Confy
{
    public static class ConfyExtensions
    {
        public static IHostBuilder UseLocalConfigServer(this IHostBuilder hostBuilder, Action<GitSettings> setupAction)
        {
            hostBuilder.ConfigureServices(((context, collection) =>
            {
                var gitSettings = new GitSettings();
                
                setupAction?.Invoke(gitSettings);

                collection.AddSingleton(gitSettings);

                collection.AddRepositoryContext();

                collection.AddHostedService<ConfyHostedService>();
            }));

            return hostBuilder;
        }
    }
}
