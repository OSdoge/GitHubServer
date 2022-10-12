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
            Console.WriteLine(s);

            var (userAgent, token, data, owner, repoName, filePath) = ("Ng-Yu-Heng", "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", "please work :C", "OSdoge", "GitHubServer", "testing.txt");

            foreach (var i in await gh.CreateFile(userAgent, token, data, owner, repoName, filePath))
            {
                Console.WriteLine(i);
            }
        }
    }
}
