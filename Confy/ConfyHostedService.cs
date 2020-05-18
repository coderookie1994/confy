using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Confy.Git;
using Confy.Infrastructure.models;

namespace Confy
{
    public class ConfyHostedService : IHostedService
    {
        private readonly ICloneRepository _cloneRepository;
        private readonly GitSettings _settings;

        public ConfyHostedService(ICloneRepository cloneRepository, GitSettings settings)
        {
            _cloneRepository = cloneRepository;
            _settings = settings;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Start cloning from git

            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
