using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

        public string UserNameEnvironmentVariableName { get; set; }
        public string AuthTokenEnvironmentVariableName { get; set; }
        public bool AlwaysCloneOnStart { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return new GitConfigurationProvider(this, new WindowsRepository());
            return new GitConfigurationProvider(this, new WindowsRepository());
        }
    }
}
