using System;
using System.Text;
using System.Threading.Tasks;
using GitHubServer;

namespace GitHubServer.Output
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GitHubServer gh = new GitHubServer();

            var s = await gh.GetFile("OSdoge", "GitHubServer", "LICENSE.txt");
            Console.WriteLine(Encoding.UTF8.GetString(Convert.FromBase64String(s.Content)));
        }
    }
}
