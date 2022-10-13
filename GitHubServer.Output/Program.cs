using System;
using System.Text;
using System.Threading.Tasks;
using GitHubServer;
using DotNetEnv;
using System.Diagnostics;

namespace GitHubServer.Output
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GitHubServer gh = new GitHubServer();
            Env.TraversePath().Load();
            var USERNAME = Env.GetString("USERNAME");
            var TOKEN = Env.GetString("AUTH_TOKEN");

            await gh.CreateFile(USERNAME, TOKEN, "among us", USERNAME, "github-api-testing", "t.txt");
        }
    }
}
