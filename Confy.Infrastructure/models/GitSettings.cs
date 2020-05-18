using System;
using System.Collections.Generic;
using System.Text;

namespace Confy.Infrastructure.models
{
    public class GitSettings
    {
        public string Uri { get; set; }
        public string Branch { get; set; } = "master";
        public string UsernameEnvironmentVariable { get; set; }
        public string PasswordEnvironmentVariable { get; set; }
    }
}
