using System;
using Confy.Git;
using Microsoft.Extensions.Configuration;

namespace Confy
{
    public static class ConfyExtensions
    {
        public static IConfigurationBuilder AddGitSource(this IConfigurationBuilder builder,
            Action<GitConfigurationSource> setupAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var source = new GitConfigurationSource();

            setupAction.Invoke(source);

            builder.Add(source);

            return builder;
        }
    }
}
