using P02_DatabaseFirst.Data;
using System;
using System.IO;
using System.Linq;

namespace P10_DepartmentsWithMoreThan5Employee
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var departments = context.Departments
                    .Where(d => d.Employees.Count > 5)
                    .OrderBy(d => d.Employees.Count).ThenBy(d => d.Name)
                    .Select(d => new { MangerName = d.Manager.FirstName + " " + d.Manager.LastName, depName = d.Name ,Employees = d.Employees.ToArray() });

                using (StreamWriter sw = new StreamWriter(@"..\result.txt"))
                {
                    foreach (var dep in departments)
                    {
                        sw.WriteLine($"{dep.depName} - {dep.MangerName}");
                        foreach (var emp in dep.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                        {
                            sw.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
                        }
                        sw.WriteLine(new string('-', 10));
                    }
                }
            }
        }
    }
}
