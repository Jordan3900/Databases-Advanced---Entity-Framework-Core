using P02_DatabaseFirst.Data;
using System;
using System.IO;
using System.Linq;

namespace P03_EmployeeFullInformation
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var employees = context.Employees.Select(e => new { e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary, e.EmployeeId}).ToArray();

                using (StreamWriter writer = new StreamWriter(@"C:\Users\pmyor\Source\Repos\EF_Core_Introduction\P02_DatabaseFirst\Result.txt"))
                {
                    foreach (var employee in employees.OrderBy(e => e.EmployeeId))
                    {
                        writer.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
                    }
                }
            }
        }
    }
}
