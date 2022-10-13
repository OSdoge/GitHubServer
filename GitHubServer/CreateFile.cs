using Newtonsoft.Json.Linq;
using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitHubServer
{
    public partial class GitHubServer
    {
        public async Task CreateFile(string userAgent, string token, string data, string owner, string repoName, string filePath, string message = "N/A", string branch = "main")
        {
            var tokenAuth = new Credentials(token);
            var client = new GitHubClient(new ProductHeaderValue(userAgent));
            string url = $"https://api.github.com/repos/{owner}/{repoName}/contents/{filePath}";

            //Authentication
            client.Credentials = tokenAuth;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);

            var request = new CreateFileRequest(message, data);
            var result = (await client.Repository.Content.CreateFile(owner, repoName, filePath, request)).Content;
        }
    }
}