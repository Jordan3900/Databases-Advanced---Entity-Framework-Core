using System;
using System.Globalization;
using System.Linq;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P07_EmployeesAndProjects
{
    class EmployeesAndProjects
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var employees = context.Employees
                    .Where(x => x.EmployeesProjects.Any(s => s.Project.StartDate.Year >= 2001 && s.Project.EndDate.Value.Year <= 2003))
                    .Select(x => new
                    {
                        EmployeeName = x.FirstName + " " + x.LastName,
                        ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                        Projects = x.EmployeesProjects.Select(s => new
                        {
                            ProjectName = s.Project.Name,
                            StartDate = s.Project.StartDate,
                            EndtDate = s.Project.EndDate
                        })
                    }).Take(30).ToArray();

                foreach (var e in employees)    
                {
                    Console.WriteLine($"{e.EmployeeName} - Manager: {e.ManagerName}");

                    foreach (var p in e.Projects)
                    {
                        Console.WriteLine($"--{p.ProjectName} - {p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} - {p.EndtDate?.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)?? "not finished"}");
                    }
                }
            }
        }
    }
}
