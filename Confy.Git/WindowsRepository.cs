using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Confy.Git.Interfaces;
using LibGit2Sharp;

namespace Confy.Git
{
    internal class WindowsRepository : IPlatformRepository
    {
        private readonly GitOperations _operations;

        public WindowsRepository(GitOperations operations)
        {
            _operations = operations;
        }

        public string Clone(GitConfigurationSource source)
        {
            var cloneOptions = new Models.CloneOptions();

            if(source.CloneOptions == null)
                throw new ArgumentNullException($"Required action ${nameof(cloneOptions)}");

            source.CloneOptions(cloneOptions);

            var dirPath = Environment.ExpandEnvironmentVariables(@"%userprofile%\AppData\Local\Temp\Confy");

            dirPath = !string.IsNullOrEmpty(cloneOptions.CloneSubDir)
                ? dirPath + $"\\{cloneOptions.CloneSubDir}"
                : dirPath;

            var repoPath = "";

            if(cloneOptions.AlwaysCloneOnStart)
                repoPath = _operations.Clone(source, dirPath + Guid.NewGuid());

            else if (cloneOptions.AlwaysCloneOnStart is false)
            {
                var subDir = string.IsNullOrEmpty(cloneOptions.CloneSubDir)
                    ? throw new ArgumentNullException($"Missing parameter {nameof(cloneOptions.CloneSubDir)}")
                    : cloneOptions.CloneSubDir;

                var directory = Directory
                    .GetDirectories(dirPath.Replace(cloneOptions.CloneSubDir, "")).FirstOrDefault(p => p.Contains(cloneOptions.CloneSubDir));
                
                if(string.IsNullOrEmpty(directory))
                    repoPath = _operations.Clone(source, dirPath + Guid.NewGuid());
                else
                    repoPath = directory;
            }


            if (repoPath.EndsWith(@".git\"))
                repoPath = repoPath.Replace(@".git\", string.Empty);

            return repoPath;
        }

    }
}
