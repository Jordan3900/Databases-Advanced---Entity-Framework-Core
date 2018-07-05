using P02_DatabaseFirst.Data;
using System;
using System.Linq;
using System.Globalization;

namespace P11_FindLatest10Projects
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var latestProjects = context.Projects.OrderByDescending(p => p.StartDate).Take(10);

                foreach (var p in latestProjects.OrderBy(p => p.Name))
                {
                    Console.WriteLine(p.Name);
                    Console.WriteLine(p.Description);
                    Console.WriteLine(p.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture));
        
                }

            }
        }
    }
}
