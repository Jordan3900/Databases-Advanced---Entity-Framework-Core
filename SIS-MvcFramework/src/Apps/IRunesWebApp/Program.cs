namespace IRunesWebApp
{
    using SIS.MvcFramework;
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            WebHost.Start(new StartUp());
        }
    }
}
