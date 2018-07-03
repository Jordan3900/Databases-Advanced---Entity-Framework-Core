using MiniORM.App.Data.Entities;
using System;
using System.Linq;

namespace MiniORM.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var connectingString = @"Server=.\SQLEXPRESS;Database=MiniORM;Integrated Security=true";

            var context = new SoftUniDbContext(connectingString);

            context.Employees.Add(new Employees
            {
                FirstName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true,
            });

            var employee = context.Employees.Last();
            employee.FirstName = "Modified";

            context.SaveChanges();
        }
    }
}
