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
            var repoPath = Repository.Clone(
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

            if (repoPath.EndsWith(@".git/"))
                repoPath = repoPath.Replace(@".git/", string.Empty);
            return repoPath;
        }
    }
}