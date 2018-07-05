using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace P12_IncreaseSalary
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] departments = new string[] { "Engineering", "Tool Design", "Marketing", "Information Services" };

            using (var context = new SoftUniContext())
            {
                var employees = context.Employees
                    .Where(x => departments.Any(s => s == x.Department.Name))
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName).ToArray();

                foreach (var e in employees)
                {
                    e.Salary *= 1.12m;
                    Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
                }

                context.SaveChanges();
            }
        }
    }
}
