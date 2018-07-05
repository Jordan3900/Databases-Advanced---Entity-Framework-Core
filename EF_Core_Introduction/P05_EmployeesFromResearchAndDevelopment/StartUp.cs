using P02_DatabaseFirst.Data;
using System;
using System.Linq;

namespace P05_EmployeesFromResearchAndDevelopment
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var selectedEmployees = context.Employees.Where(e => e.Department.Name == "Research and Development")
                    .OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName).
                    Select(e => new { e.FirstName, e.LastName, dept = e.Department.Name, e.Salary}).ToArray();

                foreach (var employee in selectedEmployees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.dept} - ${employee.Salary:f2}");
                }
            }
        }
    }
}
