namespace Spike.IronyFtpCommandParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Irony;
    using Irony.Interpreter;
    using Irony.Parsing;

    [Language("AZFTP", "1.0", "Azure Ftp For SharePoint Language")]
    public class FtpGrammar : Grammar
    {
        /// <summary>
        /// Class constructor and grammar definition
        /// </summary>
        public FtpGrammar() : base(false)
        {
            // declare keywords
            var openKeyword = ToTerm("open");
            var changeDirKeyword = ToTerm("cd");
            var dirKeyword = ToTerm("dir");
            var listKeyword = ToTerm("ls");
            var closeKeyword = ToTerm("close");
            var getKeyword = ToTerm("get");
            var byeKeyword = ToTerm("bye");
            var backfolderKeyword = ToTerm("..");
            var rootfolderKeyword = ToTerm(@"\");

            // declare non-terminals
            var program = new NonTerminal("program");
            var commandList = new NonTerminal("commandList");
            var command = new NonTerminal("command");
            var openCommand = new NonTerminal("open");
            var changeDirCommand = new NonTerminal("cd");
            var dirCommand = new NonTerminal("dir");
            var closeCommand = new NonTerminal("close");
            var getCommand = new NonTerminal("get");
            var byeCommand = new NonTerminal("byeCommand");
            var url = new NonTerminal("url");
            var folderName = new NonTerminal("folderName");

            var quotedUrl = new StringLiteral("quotedUrl", "\"");
            var unquotedUrl = new IdentifierTerminal("unquotedUrl");
            var quotedIdentifier = new StringLiteral("quotedIdentifier", "\"");
            var unquotedIdentifier = new IdentifierTerminal("unquotedIdentifier");
            var filename = new RegexBasedTerminal("filename", @"[a-zA-Z0-9\.\-_]+"); // note: space not allowed.

            // grammar rules
            program.Rule = commandList;
            commandList.Rule = this.MakePlusRule(commandList, null, command);
            command.Rule = openCommand | changeDirCommand | dirCommand | closeCommand | getCommand | byeCommand;

            openCommand.Rule = openKeyword + url + this.NewLine; // string_literal + string_literal +
            changeDirCommand.Rule = changeDirKeyword + rootfolderKeyword + this.NewLine | changeDirKeyword + backfolderKeyword + this.NewLine | changeDirKeyword + folderName + this.NewLine;
            dirCommand.Rule = (dirKeyword | listKeyword) + this.NewLine;
            closeCommand.Rule = closeKeyword + this.NewLine;
            getCommand.Rule = getKeyword + unquotedIdentifier + this.NewLine; // vai ser preciso usar uma regex para o nome do ficheiro
            byeCommand.Rule = byeKeyword + this.NewLine;

            //// string regex = @"^[a-zA-Z0-9\.-_ ]+$" (cuidado com ., .., ...) e os espaços
            //// inválidos: \/:*?"<>|

            url.Rule = quotedUrl | unquotedUrl;
            folderName.Rule = quotedIdentifier | filename; 

            // remove these notes as children in the AST
            this.MarkPunctuation("open", "dir", "ls", "close", "get", "cd", "bye");
            this.Root = program;

            // LanguageFlags |= LanguageFlags.CreateAst;
        }
    }
}
