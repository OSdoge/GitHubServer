using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Octokit;
using System.Security.Policy;

namespace GitHubServer
{
    public class GitHubServer
    {
        public async Task<string> GetFile(string owner, string repoName, string filePath, string userAgent = "doge")
        {
            var url = $"https://api.github.com/repos/{owner}/{repoName}/contents/{filePath}";

            //Get request
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            var content = await (await client.GetAsync(url)).Content.ReadAsStringAsync();

            //Convert Base64 to 8-Bit Integer Array
            var jsonList = JObject.Parse(content);

            if (jsonList != null)
            {
                var text = Encoding.UTF8.GetString(Convert.FromBase64String(jsonList["content"].ToString()));
                return text;
            }

            throw new Exception("json list is null");
        }

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
                abc = new bool[]  { false, result.IsCanceled, result.IsFaulted, result.IsCompleted };
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
                abc = new bool[] { true, result.IsCanceled, result.IsFaulted, result.IsCompleted};
            }

            return abc;

        }
    }
}
