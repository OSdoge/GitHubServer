using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public async Task<GetFileResponse> GetFile(string owner, string repoName, string path, string userAgent = "doge")
        {
            var url = $"https://api.github.com/repos/{owner}/{repoName}/contents/{path}";

            //Get request
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            var content = await (await client.GetAsync(url)).Content.ReadAsStringAsync();
            var file = JsonConvert.DeserializeObject<GetFileResponse>(content);

            return file;
        }
    }
}
