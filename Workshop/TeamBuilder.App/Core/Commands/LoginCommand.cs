namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class LoginCommand : ICommand
    {
        public string Execute(string[] inputArgs)
        {
            Checks.CheckLength(2, inputArgs);

            string username = inputArgs[0];
            string password = inputArgs[1];

            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            User user = this.GetUserByCredetials(username, password);

            if (user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }

            AuthenticationManager.Login(user);

            return $"Users {user.Username} successfully logged in!";
        }

        private User GetUserByCredetials(string username, string password)
        {
            using (var context = new TeamBuilderContext())
            {
                var user = context.Users
                    .Where(x => x.Username == username && x.Password == password && !x.IsDeleted)
                    .SingleOrDefault();

                return user;
            }
        }
    }
}
