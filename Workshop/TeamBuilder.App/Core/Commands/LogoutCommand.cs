namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;

    public class LogoutCommand : ICommand
    {
        public string Execute(string[] inputArgs)
        {
            Checks.CheckLength(0, inputArgs);
            AuthenticationManager.Authorize();
            User currentUser = AuthenticationManager.GetCurrentUser();

            AuthenticationManager.Logout(currentUser);

            return $"User {currentUser.Username} successfully logged out!";
        }
    }
}
