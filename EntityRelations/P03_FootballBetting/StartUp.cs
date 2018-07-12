using P03_FootballBetting.Data;
using P03_FootballBetting.Data.Models;
using System;

namespace P03_FootballBetting
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new FootballBettingContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
