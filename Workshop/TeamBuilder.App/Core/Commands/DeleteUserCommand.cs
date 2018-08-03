namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class DeleteUserCommand : ICommand
    {
        public string Execute(string[] inputArgs)
        {
            Checks.CheckLength(0, inputArgs);
            AuthenticationManager.Authorize();

            User currentUser = AuthenticationManager.GetCurrentUser();

            using (var context = new TeamBuilderContext())
            {
                currentUser.IsDeleted = true;
                context.SaveChanges();

                AuthenticationManager.Logout(currentUser);
            }

            return $"User {currentUser.Username} was deleted successfully!";
        }
    }
}
