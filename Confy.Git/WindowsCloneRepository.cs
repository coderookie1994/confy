using System;
using System.IO;
using System.Runtime.InteropServices;
using Confy.Infrastructure.models;
using LibGit2Sharp;

namespace Confy.Git
{
    class WindowsCloneRepository : ICloneRepository
    {
        private readonly GitSettings _gitSettings;

        public WindowsCloneRepository(GitSettings gitSettings)
        {
            _gitSettings = gitSettings;
        }

        public string Clone()
        {
            return Repository.Clone(Path.Combine(Environment.ExpandEnvironmentVariables(@"%userprofile%\AppData\Local\Temp")),
                _gitSettings.Uri, new CloneOptions()
                {
                    BranchName = _gitSettings.Branch, CredentialsProvider =
                        (url, fromUrl, types) => new UsernamePasswordCredentials()
                        {
                            Username = Environment.GetEnvironmentVariable(_gitSettings.UsernameEnvironmentVariable),
                            Password = Environment.GetEnvironmentVariable(_gitSettings.PasswordEnvironmentVariable)
                        }
                });
        }
    }
}
