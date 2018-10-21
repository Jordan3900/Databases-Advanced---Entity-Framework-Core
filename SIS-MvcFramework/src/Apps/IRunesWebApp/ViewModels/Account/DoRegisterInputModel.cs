namespace IRunesWebApp.ViewModels.Account
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DoRegisterInputModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
    }
}
