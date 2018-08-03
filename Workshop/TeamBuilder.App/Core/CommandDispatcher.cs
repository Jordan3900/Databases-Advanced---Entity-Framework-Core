namespace TeamBuilder.App.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Core.Commands;

    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            string result = string.Empty;

            string[] inputArgs = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            string commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
            inputArgs = inputArgs.Skip(1).ToArray();
            ICommand command;

            switch (commandName)
            {
                case "Exit":
                    command = new ExitCommand();
                    command.Execute(inputArgs);
                        break;
                case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand();
                    result = registerUser.Execute(inputArgs);
                    break;
                case "Login":
                    LoginCommand login = new LoginCommand();
                    result = login.Execute(inputArgs);
                    break;
                case "Logout":
                    LogoutCommand logout = new LogoutCommand();
                    result = logout.Execute(inputArgs);
                    break;
                case "DeleteUser":
                    DeleteUserCommand deleteUser = new DeleteUserCommand();
                    result = deleteUser.Execute(inputArgs);
                    break;
                default:
                    throw new NotSupportedException($"Command {commandName} not supported!");
            }

            return result;
        }
    }
}
