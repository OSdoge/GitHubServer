using Newtonsoft.Json.Linq;
using Octokit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitHubServer
{
    public partial class GitHubServer
    {
        public async Task<bool[]> CreateFile(string userAgent, string token, string data, string owner, string repoName, string filePath, string message = "N/A", string branch = "main")
        {
            var tokenAuth = new Credentials(token);
            var client = new GitHubClient(new ProductHeaderValue(userAgent));
            string url = $"https://api.github.com/repos/{owner}/{repoName}/contents/{filePath}";
            //Authentication
            client.Credentials = tokenAuth;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);

            bool[] abc;
            //Creating & Updating File
            try
            {
                //Create CreateFileRequest
                var createFileRequest = new CreateFileRequest(message, data);
                //Create File
                var result = client.Repository.Content.CreateFile(owner, repoName, filePath, createFileRequest);
                abc = new bool[] { false, result.IsCanceled, result.IsFaulted, result.IsCompleted };
            }
            catch
            {
                //Get Sha
                var content = await (await httpClient.GetAsync(url)).Content.ReadAsStringAsync();
                var jsonList = JObject.Parse(content);
                var sha = "";
                try
                {
                    sha = Encoding.UTF8.GetString(Convert.FromBase64String(jsonList["sha"].ToString()));
                }
                catch
                {
                    throw new Exception("Sha not found.");
                }

                //Create UpdateFileRequest
                var updateFileRequest = new UpdateFileRequest(message, data, sha);

                //Update File
                var result = client.Repository.Content.UpdateFile(owner, repoName, filePath, updateFileRequest);
                abc = new bool[] { true, result.IsCanceled, result.IsFaulted, result.IsCompleted };
            }

            return abc;
        }
    }
}
