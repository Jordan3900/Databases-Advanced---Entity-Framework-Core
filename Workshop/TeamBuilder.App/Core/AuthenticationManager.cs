namespace TeamBuilder.App.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;

    public class AuthenticationManager
    {
        private static User currentUser;

        public static void Login(User user)
        {
            currentUser = user;
        }

        public static void Logout(User user)
        {
            if (!IsAuthenticated())
            {
                Authorize();
            }
            user = null;
        }

        public static User GetCurrentUser()
        {
            if (!IsAuthenticated())
            {
                Authorize();
            }

            return currentUser;
        }

        public static bool IsAuthenticated()
        {
            return currentUser != null;
        }

        public static void Authorize()
        {
            if (currentUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }
    }
}
