using System;
using System.Data.SqlClient;
using _01.InitialSetUp;

namespace _02.VilianNames
{
    class VilianNames
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string vilianInfo = @"SELECT v.[Name], COUNT(m.Id) AS [MinionsCount] FROM Villains AS v JOIN MinionsVillains AS mv ON mv.VillainId = v.Id JOIN Minions AS m on m.Id = mv.MinionId
                                    GROUP BY v.[Name] HAVING COUNT(mv.MinionId) >= 3 ORDER BY COUNT(m.Id) DESC";

                using (SqlCommand command = new SqlCommand(vilianInfo, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} -> {reader[1]}");
                        }
                    }
                }

                connection.Close();
            }
            
        }
    }
}
