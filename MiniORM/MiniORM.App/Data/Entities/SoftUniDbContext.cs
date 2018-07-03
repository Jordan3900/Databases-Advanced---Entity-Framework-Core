namespace MiniORM.App.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Entities;

    public class SoftUniDbContext : DbContex
    {
        public SoftUniDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Employees> Employees { get; }

        public DbSet<Department> Departments { get; }

        public DbSet<Projects> Projects { get; }

        public DbSet<EmployeesProject> EmployeesProjects { get; }
    }
}
