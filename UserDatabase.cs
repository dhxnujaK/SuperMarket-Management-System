using System;
using System.Data.SQLite;
using System.IO;

public class UserDatabase
{
    private string dbFile = "SuperMarketDB.db";
    private string connectionString;

    public UserDatabase()
    {
        connectionString = $"Data Source={dbFile};Version=3;";
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        if (!File.Exists(dbFile)) // Check if the database file exists
        {
            SQLiteConnection.CreateFile(dbFile);
            Console.WriteLine("Database Created!");
            CreateTables();
        }
    }

    private void CreateTables()
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string query = @"CREATE TABLE IF NOT EXISTS Customers (
                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT NOT NULL,
                                NIC TEXT UNIQUE NOT NULL,
                                ContactNumber TEXT NOT NULL
                            );";

            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Table Created!");
        }
    }

    public SQLiteConnection GetConnection()
    {
        return new SQLiteConnection(connectionString);
    }
}
