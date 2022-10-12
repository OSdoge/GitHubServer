using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitHubServer
{
    public partial class GitHubServer
    {
        public async Task<string> GetFile(string owner, string repoName, string path, string userAgent = "doge")
        {
            var url = $"https://api.github.com/repos/{owner}/{repoName}/contents/{path}";

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
    }
}
