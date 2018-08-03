using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] inputArgs)
        {
            Checks.CheckLength(0, inputArgs);

            Environment.Exit(0);

            return "Bye!";
        }
    }
}
