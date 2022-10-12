using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubServer
{
    public class GetFileResponse
    {
        public string Type { get; set; }
        public string Encoding { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public string Sha { get; set; }
        public string Url { get; set; }
        public string Git_Url { get; set; }
        public string Html_Url { get; set; }
        public string Download_Url { get; set; }
        public Links _Links { get; set; }
    }
}
