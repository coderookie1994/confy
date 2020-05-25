using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Confy.Git.Interfaces;
using LibGit2Sharp;

namespace Confy.Git
{
    internal class WindowsRepository : IPlatformRepository
    {
        public string Clone(GitConfigurationSource source)
        {
            return Repository.Clone(
                source.Url,
                Path.Combine(Environment.ExpandEnvironmentVariables(@"%userprofile%\AppData\Local\Temp\Confy_" + Guid.NewGuid())),
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
