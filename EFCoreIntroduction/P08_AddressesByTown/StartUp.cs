using P02_DatabaseFirst.Data;
using System;
using System.Linq;

namespace P08_AddressesByTown
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var adresses = context.Addresses
                    .Select(a => new { countOfEmp = a.Employees.Count, townName = a.Town.Name, addrText = a.AddressText })
                    .OrderByDescending(a => a.countOfEmp).ThenBy(a => a.townName).ThenBy(a => a.addrText).Take(10);

                foreach (var addr in adresses)
                {
                    Console.WriteLine($"{addr.addrText}, {addr.townName} - {addr.countOfEmp} employees");
                }
            }
        }
    }
}
