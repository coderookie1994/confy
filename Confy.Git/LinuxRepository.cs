using System;
using System.IO;
using Confy.Git.Interfaces;
using LibGit2Sharp;

namespace Confy.Git
{
    class LinuxRepository : IPlatformRepository
    {
        public string Clone(GitConfigurationSource source)
        {
            return Repository.Clone(
                source.Url,
                Path.Combine(Environment.ExpandEnvironmentVariables(@"/tmp/Confy_" + Guid.NewGuid())),
                new CloneOptions()
                {
                    BranchName = source.Branch,
                    CredentialsProvider = (url, fromUrl, types) => new UsernamePasswordCredentials()
                    {
                        Username = source.UserNameEnvironmentVariableName,
                        Password = source.AuthTokenEnvironmentVariableName
                    }
                });
        }
    }
}