using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite
{
    class DataAccess
    {
        public static void InitializeDatabase()
        {
            //await ApplicationData.Current.LocalFolder.CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
            //string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS " +
                    "Person (Id INTEGER PRIMARY KEY, " +
                    "Fname NVARCHAR(30) NULL," +
                    "Lname NVARXHAR(30) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddData(string Fname, string Lname)
        {
            using (SqliteConnection db =
              new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Person VALUES (NULL, @firstName, @lastName);";
                insertCommand.Parameters.AddWithValue("@firstName", Fname);
                insertCommand.Parameters.AddWithValue("@lastName", Lname);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();

            using (SqliteConnection db =
               new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Person", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add("(id = "+query.GetString(0)+ ") Name = "+ query.GetString(1)+ " Lastname = "+ query.GetString(2));
                }

                db.Close();
            }

            return entries;
        }
    }
}
