using System;
using System.Data.SqlClient;
using _01.InitialSetUp;

namespace _09.IncreaseAgeStoredProcedure
{
    class IncreaseAgeStoredProcedure
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection( Configuration.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("usp_GetOlder", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure


                })
                {
                    command.Parameters.AddWithValue("@Id", id);
                    string result = (string)command.ExecuteScalar();
                    Console.WriteLine(result);
               
                }
                    connection.Close();
            }
        }
    }
}
