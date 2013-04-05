using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony;
using Irony.Parsing;
using Irony.Interpreter;
using Irony.Ast;
 
namespace Spike.Irony
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ftp Irony Console Sample";
            Console.WriteLine("Ftp Irony Console Sample.");
            Console.WriteLine("");
            Grammar grammar = new FtpGrammar();
            var language = new LanguageData(grammar);
            var runtime = new LanguageRuntime(language);
            var commandLine = new CommandLine(runtime);
            commandLine.Prompt = "AZFTP>";
            commandLine.Run();
        }
    }
}
