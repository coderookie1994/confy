using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApiSample;
using Xunit;

namespace Confy.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _app;
        private readonly HttpClient _httpClient;

        public UnitTest1(WebApplicationFactory<Startup> app)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT","Development");

            _app = app;
            _httpClient = _app.CreateClient();
        }

        [Fact]
        public void Test1()
        {
            var httpResponse = _httpClient.GetAsync("weatherforecast").GetAwaiter().GetResult();
        }
    }
}
