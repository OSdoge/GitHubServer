using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Octokit;

namespace GitHubServer
{
    public class GHSever
    {
        public async Task<string> GetFile(string owner, string repoName, string filePath, string branch)
        {
            string url = "https://api.github.com/repos/" + owner + "/" + repoName + "/contents/" + filePath;

            //Get request
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "doge");
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            //Convert Base64 to 8-Bit Integer Array
            var jsonList = JObject.Parse(content);
            if (jsonList != null)
            {
                byte[] text = Convert.FromBase64String(jsonList["content"].ToString());
                string newtext = Encoding.UTF8.GetString(text);
                return newtext;
            }
            else
            {
                throw new InvalidOperationException("jsonList is null!");
            }
        }
    }
}
