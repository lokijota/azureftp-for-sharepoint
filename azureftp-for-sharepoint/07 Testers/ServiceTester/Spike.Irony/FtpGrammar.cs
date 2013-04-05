using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony;
using Irony.Interpreter;
using Irony.Parsing;

namespace Spike.Irony
{
    [Language("AZFTP", "1.0", "Azure Ftp For SharePoint Language")]
    public class FtpGrammar : Grammar
    {

        /// <summary>
        /// Class constructor and grmamar definition
        /// </summary>
        public FtpGrammar() : base(false)
        {
            var open = ToTerm("open");
            var cd = ToTerm("cd");
            var dir = ToTerm("dir");
            var close = ToTerm("close");
            var get = ToTerm("get");
            var backfolder = ToTerm("..");
            var rootfolder = ToTerm(@"\");

            var program = new NonTerminal("program");
            var commandList = new NonTerminal("commandList");
            var command = new NonTerminal("command");
            var openCommand = new NonTerminal("open");
            var cdCommand = new NonTerminal("cd");
            var dirCommand = new NonTerminal("dir");
            var closeCommand = new NonTerminal("close");
            var getCommand = new NonTerminal("get");

            var string_literal = new StringLiteral("string", ",", StringOptions.AllowsDoubledQuote);
            //new RegexBasedTerminal

            var url = new DsvLiteral("url", TypeCode.String);
            var username = new DsvLiteral("username", TypeCode.String);
            var password = new DsvLiteral("password", TypeCode.String);
            var foldername = new DsvLiteral("foldername", TypeCode.String);
            var filename = new DsvLiteral("filename", TypeCode.String);

            this.Root = program;

            program.Rule = commandList;
            commandList.Rule = MakePlusRule(commandList, null, command);
            command.Rule = openCommand | cdCommand | dirCommand | closeCommand | getCommand;

            openCommand.Rule = open + string_literal + string_literal + string_literal + NewLine;
            cdCommand.Rule = cd + rootfolder + NewLine | cd + backfolder + NewLine | cd + string_literal + NewLine;
            dirCommand.Rule = dir + NewLine;
            closeCommand.Rule = close + NewLine;
            getCommand.Rule = get + filename + NewLine;

            //this.MarkPunctuation(
            //this.RegisterPunctuation("set", "camera", "size", ":", "by", "pixels", ".", "position", ",", "move");
        }
    }
}
