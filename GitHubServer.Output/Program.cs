using System;
using System.Threading.Tasks;
using GitHubServer;

namespace GitHubServer.Output
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GitHubServer gh = new GitHubServer();

            var s = await gh.GetFile("OSdoge", repoName: "GitHubServer", "LICENSE.txt");
        }
    }
}
