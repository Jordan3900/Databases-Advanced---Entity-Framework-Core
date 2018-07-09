using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Initializer;
using System;

namespace P01_HospitalDatabase
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new HospitalContext())
            {
                DatabaseInitializer.InitialSeed(context);
            }
        }
    }
}
