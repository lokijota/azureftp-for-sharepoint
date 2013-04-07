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
            string username = "vm\\administrator";
            string password = "pass@word1";
            string url = "http://createbox/Shared%20Documents/My%20Folder/My%20SubFolder";

            Console.WriteLine("Testing URL {0}", url);
            //Console.WriteLine("Web URL: {0}", GetWebUrl(url, username, password));
            Console.WriteLine("Folder URL: {0}", GetFolderUrl(url, username, password));

            Console.ReadKey();
        }

        private static string GetWebUrl(string url, string username, string password)
        {
            SharePointManager2013 spm = new SharePointManager2013(username, password);
            return spm.GetWebUrl(url);
        }

        private static string GetFolderUrl(string url, string username, string password)
        {
            SharePointManager2013 spm = new SharePointManager2013(username, password);
            return spm.GetFolderUrl(spm.GetWebUrl(url), url);
        }
    }
}
