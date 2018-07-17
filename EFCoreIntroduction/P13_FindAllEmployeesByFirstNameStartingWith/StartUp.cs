using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using System;
using System.Linq;

namespace P13_FindAllEmployeesByFirstNameStartingWith
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var employees = context.Employees.Where(x => EF.Functions.Like(x.FirstName, "Sa%")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
                                               

                foreach (var emp in employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName))
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:f2})");
                }
            }
        }
    }
}
