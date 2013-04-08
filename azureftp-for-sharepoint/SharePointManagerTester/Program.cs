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

            Console.ReadKey();
        }
    }
}
