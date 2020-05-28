using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Confy.Git.Interfaces;
using LibGit2Sharp;
using Microsoft.Extensions.Configuration;

namespace Confy.Git
{
    class GitConfigurationProvider : ConfigurationProvider
    {
        private readonly GitConfigurationSource _source;
        private readonly IPlatformRepository _platformRepository;

        public GitConfigurationProvider(GitConfigurationSource source, IPlatformRepository platformRepository)
        {
            _source = source;
            _platformRepository = platformRepository;
        }

        public override void Load()
        {
            var repoPath = _platformRepository.Clone(_source);

            var ymlFiles = Directory.GetFiles(repoPath).Where(f => f.EndsWith(".yml") || f.EndsWith(".yaml"));

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
