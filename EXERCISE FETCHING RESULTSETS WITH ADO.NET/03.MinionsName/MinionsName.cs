using System;
using _01.InitialSetUp;
using System.Data.SqlClient;

namespace _03.MinionsName
{
   public class MinionsName
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                string vilianName = GetVilianName(id, connection);

                if (vilianName == null)
                {
                    Console.WriteLine($"No villain with ID {id} exists in the database.");
                }

                else
                {
                    Console.WriteLine($"Villain: {vilianName}");
                    PrintNames(id, connection);

                }

                connection.Close();
            }
            
        }

        private static void PrintNames(int id, SqlConnection connection)
        {
            string minions = @"SELECT [Name], Age FROM Minions AS m JOIN MinionsVillains AS mv ON mv.MinionId = m.Id WHERE mv.VillainId = @Id";

            using (SqlCommand command = new SqlCommand(minions, connection))
            {
                command.Parameters.AddWithValue("id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (!reader.HasRows)    
                    {
                        Console.WriteLine("(no rows)");
                    }
                    else
                    {
                        int row = 1;

                        while (reader.Read())
                        {
                            Console.WriteLine($"{row++}. {reader[0]} {reader[1]}");
                        }
                        
                    }
                }
            }
        }

        private static string GetVilianName(int id, SqlConnection connection)
        {
            string nameSQL = @"SELECT  [Name] FROM Villains WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(nameSQL, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                return (string)command.ExecuteScalar();
            }
        }
    }
}
