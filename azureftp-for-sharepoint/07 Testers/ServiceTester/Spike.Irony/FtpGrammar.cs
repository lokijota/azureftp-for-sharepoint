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
            // declare keywords
            var openKeyword = ToTerm("open");
            var cdKeyword = ToTerm("cd");
            var dirKeyword = ToTerm("dir");
            var lsKeyword = ToTerm("ls");
            var closeKeyword = ToTerm("close");
            var getKeyword = ToTerm("get");
            var backfolderKeyword = ToTerm("..");
            var rootfolderKeyword = ToTerm(@"\");

            // declare non-terminals
            var program = new NonTerminal("program");
            var commandList = new NonTerminal("commandList");
            var command = new NonTerminal("command");
            var openCommand = new NonTerminal("open");
            var cdCommand = new NonTerminal("cd");
            var dirCommand = new NonTerminal("dir");
            var closeCommand = new NonTerminal("close");
            var getCommand = new NonTerminal("get");
            var url = new NonTerminal("url");
            var folderName = new NonTerminal("folderName");

            var quotedUrl = new StringLiteral("quotedUrl", "\"");
            var unquotedUrl = new IdentifierTerminal("unquotedUrl");
            var quotedIdentifier = new StringLiteral("quotedIdentifier", "\"");
            var unquotedIdentifier = new IdentifierTerminal("unquotedIdentifier");
            var filename = new RegexBasedTerminal("filename", @"[a-zA-Z0-9\.\-_]+"); // note: space not allowed.
            //var url = new RegexBasedTerminal("url", @"dsadas");

            // grammar rules
            program.Rule = commandList;
            commandList.Rule = MakePlusRule(commandList, null, command);
            command.Rule = openCommand | cdCommand | dirCommand | closeCommand | getCommand;

            openCommand.Rule = openKeyword + url + NewLine; // string_literal + string_literal +
            cdCommand.Rule =  cdKeyword + rootfolderKeyword + NewLine | cdKeyword + backfolderKeyword + NewLine | cdKeyword + folderName + NewLine;
            dirCommand.Rule = (dirKeyword | lsKeyword) + NewLine;
            closeCommand.Rule = closeKeyword + NewLine;
            getCommand.Rule = getKeyword + unquotedIdentifier + NewLine; // vai ser preciso usar uma regex para o nome do ficheiro

            // string regex = @"^[a-zA-Z0-9\.-_ ]+$" (cuidado com ., .., ...) e os espaços
            // inválidos: \/:*?"<>|

            url.Rule = quotedUrl | unquotedUrl;
            folderName.Rule = quotedIdentifier | filename; 

            // remove these notes as children in the AST
            MarkPunctuation("open", "dir", "ls", "close", "get", "cd");
            Root = program;
        }
    }
}
