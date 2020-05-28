using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Confy.Git.Models;
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
        public Action<CloneOptions> CloneOptions {get;set;}

        public IConfigurationProvider Build(IConfigurationBuilder builder) =>
            System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? new GitConfigurationProvider(this, new WindowsRepository(new GitOperations()))
                : System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    ? new GitConfigurationProvider(this, new LinuxRepository())
                    : new GitConfigurationProvider(this, new WindowsRepository(new GitOperations()));
    }
}
