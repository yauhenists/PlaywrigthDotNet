using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightUdemy
{
    internal class ApiTests
    {
        [Test]
        public async Task Test1()
        {
            string USER = Environment.GetEnvironmentVariable("GITHUB_USER");
            string? API_TOKEN = Environment.GetEnvironmentVariable("GITHUB_API_TOKEN");
        }
    }
}
