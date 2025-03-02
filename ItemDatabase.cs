using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DSA_SuperMarket_Management_System
{
    internal class ItemDatabase
    {
        private static string connectionString = "Data Source=supermarket.db;Version=3;";

        // Initialize the database and create the Items table if it doesn't exist
        public static void Initialize()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS Items (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ItemName TEXT NOT NULL,  -- ✅ Added Item Name
                    ItemCode TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    ExpiryDate TEXT NOT NULL,
                    ManufactureDate TEXT NOT NULL,
                    GrossAmount REAL NOT NULL,
                    NetAmount REAL NOT NULL,
                    Quantity INTEGER NOT NULL)";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to insert an item into the database
        public static void InsertItem(string itemName, string itemCode, string category, string expiryDate, string manufactureDate, double grossAmount, double netAmount, int quantity)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Items (ItemName, ItemCode, Category, ExpiryDate, ManufactureDate, GrossAmount, NetAmount, Quantity) VALUES (@ItemName, @ItemCode, @Category, @ExpiryDate, @ManufactureDate, @GrossAmount, @NetAmount, @Quantity)";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemName", itemName);  // ✅ Added Item Name
                    cmd.Parameters.AddWithValue("@ItemCode", itemCode);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                    cmd.Parameters.AddWithValue("@ManufactureDate", manufactureDate);
                    cmd.Parameters.AddWithValue("@GrossAmount", grossAmount);
                    cmd.Parameters.AddWithValue("@NetAmount", netAmount);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to retrieve all items from the database
        public static List<Item> GetItems()
        {
            List<Item> items = new List<Item>();

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Items";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new Item
                        {
                            Id = reader.GetInt32(0),
                            ItemName = reader.GetString(1),  // ✅ Added Item Name
                            ItemCode = reader.GetString(2),
                            Category = reader.GetString(3),
                            ExpiryDate = reader.GetString(4),
                            ManufactureDate = reader.GetString(5),
                            GrossAmount = reader.GetDouble(6),
                            NetAmount = reader.GetDouble(7),
                            Quantity = reader.GetInt32(8)
                        });
                    }
                }
            }
            return items;
        }
    }
}
