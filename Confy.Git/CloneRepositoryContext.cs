using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Confy.Git
{
    public class CloneRepositoryContext
    {
        private readonly OSPlatform _platform;

        public CloneRepositoryContext(OSPlatform platform)
        {
            _platform = platform;
        }
    }
}
