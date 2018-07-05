using P02_DatabaseFirst.Data;
using System;
using System.Linq;

namespace P04_EmployeesWithSalaryOver50_000
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var dbContex = new SoftUniContext())
            {
                var employeeNames = dbContex.Employees.Where(e => e.Salary > 50000).OrderBy(n => n.FirstName).Select(e => e.FirstName);

                foreach (var name in employeeNames)
                {
                    Console.WriteLine(name);
                }
            }
        }
    }
}
