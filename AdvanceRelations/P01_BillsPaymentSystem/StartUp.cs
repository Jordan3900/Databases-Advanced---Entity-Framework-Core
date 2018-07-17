using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using System;

namespace P01_BillsPaymentSystem
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new BillsPaymentContext();

            context.Database.Migrate();
        }
    }
}
