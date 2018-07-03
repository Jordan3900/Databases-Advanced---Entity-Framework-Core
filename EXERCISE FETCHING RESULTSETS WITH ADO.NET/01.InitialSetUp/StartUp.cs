using System;
using System.Data.SqlClient;
namespace _01.InitialSetUp
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string dataBaseSql = "CREATE DATABASE MinionsDB";

                ExecuteNonQuery(connection, dataBaseSql);

                connection.ChangeDatabase("MinionsDB");

                string tableCountries = @"CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))";
                string tableTowns = @"CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))";
                string tableMinions = @"CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))";
                string tableEvilness = @"CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))";
                string tableVilians = @"CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))";
                string tableMinionsVilians = @"CREATE TABLE MinionsVillains (MinionId INT FOREIGN KEY REFERENCES Minions(Id),VillainId INT FOREIGN KEY REFERENCES Villains(Id),CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))";


                ExecuteNonQuery(connection, tableCountries);
                ExecuteNonQuery(connection, tableTowns);
                ExecuteNonQuery(connection, tableMinions);
                ExecuteNonQuery(connection, tableEvilness);
                ExecuteNonQuery(connection, tableVilians);
                ExecuteNonQuery(connection, tableMinionsVilians);

                string insertCountries = @"INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')";
                string insertTowns = @"INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)";
                string insertMinions = @"INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)";
                string insertEvilFactor = @"INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')";
                string insertVilians = @"INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)";
                string insertMinionsVilians = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";

                ExecuteNonQuery(connection, insertCountries);
                ExecuteNonQuery(connection, insertTowns);
                ExecuteNonQuery(connection, insertMinions);
                ExecuteNonQuery(connection, insertEvilFactor);
                ExecuteNonQuery(connection, insertVilians);
                ExecuteNonQuery(connection, insertMinionsVilians);

                connection.Close();
            }
        }

        private static void ExecuteNonQuery(SqlConnection connection, string dataBaseSql)
        {
            using (SqlCommand command = new SqlCommand(dataBaseSql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
