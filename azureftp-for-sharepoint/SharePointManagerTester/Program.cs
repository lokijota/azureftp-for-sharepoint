using AzureFtpForSharePoint.Core.SharePointLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointManagerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string username = "vm\\administrato";
            string password = "pass@word1";
            string url = "http://createbox/search/pages/default.aspx";

            Console.WriteLine("Testing URL {0}", url);
            Console.WriteLine("Web URL: {0}", GetWebUrl(url, username, password));

            Console.ReadKey();
        }

        private static string GetWebUrl(string url, string username, string password)
        {
            SharePointManager2013 spm = new SharePointManager2013(username, password);
            return spm.GetWebUrl(url);
        }
    }
}
