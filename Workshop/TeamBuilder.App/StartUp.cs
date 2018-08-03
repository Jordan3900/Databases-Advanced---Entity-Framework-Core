namespace TeamBuilder.App
{
    using System;
    using TeamBuilder.App.Core;
    using TeamBuilder.Data;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new TeamBuilderContext();
            var commandDistpatcher = new CommandDispatcher();
            var engine = new Engine(commandDistpatcher);
            engine.Run();
        }
    }
}
