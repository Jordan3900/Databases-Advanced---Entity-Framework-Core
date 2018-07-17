using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;
using System;
using System.Linq;

namespace P06_AddingANewAddressAndUpdatingEmployee
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var adress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            
            using (var context = new SoftUniContext())
            {
                var addresses = context.Employees.Select(e => e.Address).OrderByDescending(a=> a.AddressId).ToArray().Take(10);

                foreach (var adr in addresses)
                {
                    Console.WriteLine(adr.AddressText);
                }

               
            }
        }
    }
}
