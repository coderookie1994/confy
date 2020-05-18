using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibGit2Sharp;
using Microsoft.Extensions.Configuration;

namespace Confy.Git
{
    class GitConfigurationProvider : ConfigurationProvider
    {
        private readonly GitConfigurationSource _source;

        public GitConfigurationProvider(GitConfigurationSource source)
        {
            _source = source;
        }

        public override void Load()
        {
            var repoPath = Repository.Clone(
                _source.Url,
                Path.Combine(Environment.ExpandEnvironmentVariables(@"%userprofile%\AppData\Local\Temp\Confy")));

            if (repoPath.EndsWith(@".git\"))
                repoPath = repoPath.Replace(@".git\", String.Empty);

            var ymlFiles = Directory.GetFiles(repoPath).Where(f => f.EndsWith(".yml") || f.EndsWith("yaml"));

            var filteredYml = ymlFiles.FirstOrDefault(f =>
                f.Contains(_source.AppName) || f.Contains($"{_source.AppName}-{_source.HostingEnvironment}"));

            if (filteredYml != null)
                using (var fs = File.Open(filteredYml, FileMode.Open))
                {
                    var parser = new YamlConfigurationFileParser();

                    Data = parser.Parse(fs);
                }
        }
    }
}
