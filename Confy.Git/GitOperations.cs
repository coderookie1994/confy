using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LibGit2Sharp;

namespace Confy.Git
{
    internal class GitOperations
    {
        internal string Clone(GitConfigurationSource source, string dirPath)
        {
            return Repository.Clone(
                source.Url,
                Path.Combine(
                    Environment.ExpandEnvironmentVariables(dirPath)),
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
