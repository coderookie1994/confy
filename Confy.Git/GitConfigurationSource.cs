using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Confy.Git
{
    public class GitConfigurationSource : IConfigurationSource
    {
        public string Url { get; set; }
        public string Branch { get; set; }

        public string AppName { get; set; }
        public string HostingEnvironment { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new GitConfigurationProvider(this);
        }
    }
}
