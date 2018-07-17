using P02_DatabaseFirst.Data;
using System;
using System.Linq;

namespace P14_DeleteProjectById
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                //var projects = context.emplo.Where(p => p.ProjectId == 2);
                //context.EmployeesProjects.RemoveRange(projects);
                //var project = context.Projects.Find(2);
                //context.Projects.Remove(project);
                //context.SaveChanges();

                var projects = context.Projects.Take(10).ToArray();
                foreach (var p in projects)
                {
                    Console.WriteLine(p.Name);
                }
            }
        }
    }
}
