namespace Spike.IronyFtpCommandParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Irony;
    using Irony.Ast;
    using Irony.Interpreter;
    using Irony.Interpreter.Ast;
    using Irony.Interpreter.Evaluator;
    using Irony.Parsing;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Ftp Irony Console Sample";
            Console.WriteLine("Ftp Irony Console Sample.");
            Console.WriteLine(string.Empty);
            Grammar grammar = new FtpGrammar();
            var language = new LanguageData(grammar);
            var runtime = new LanguageRuntime(language);

            Parser parser = new Parser(grammar); // necessário

            while (true)
            {
                Console.Write("AZFTP> ");
                string input = Console.ReadLine();
                ParseTree tree = parser.Parse(input + System.Environment.NewLine);

                // notajota: the interpretation of the input command must be externalized and uniformized to transform the Ast into service calls
                // if command was recognized
                if (tree.ParserMessages.Count == 0) 
                {
                    if (tree.Root.ChildNodes[0].ChildNodes[0].ChildNodes[0].Term.Name == "byeCommand")
                    {
                        return;
                    }
                }

                Console.WriteLine("## parser messages: " + tree.ParserMessages.Count);
                foreach (LogMessage lm in tree.ParserMessages)
                {
                    Console.WriteLine("## parser messages: " + lm.Message);
                }

                Console.WriteLine("## parser time (ms): " + tree.ParseTimeMilliseconds);
            }
        }
    }
}
