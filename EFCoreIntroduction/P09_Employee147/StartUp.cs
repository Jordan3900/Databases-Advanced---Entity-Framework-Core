using P02_DatabaseFirst.Data;
using System;
using System.Linq;

namespace P09_Employee147
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var projects = context.Projects.Where(p => p.EmployeesProjects.Any(e => e.EmployeeId == 147));
                var employee = context.Employees.Where(e => e.EmployeeId == 147).First();

                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                foreach (var p in projects.OrderBy(p => p.Name))
                {
                    Console.WriteLine(p.Name);
                }
            }
        }
    }
}
