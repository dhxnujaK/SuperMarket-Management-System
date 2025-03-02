using System;

namespace DSA_SuperMarket_Management_System
{
    public class Item : IComparable<Item>
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Category { get; set; }
        public string ExpiryDate { get; set; }
        public string ManufactureDate { get; set; }
        public double GrossAmount { get; set; }
        public double NetAmount { get; set; }
        public int Quantity { get; set; }

        // Default CompareTo (Sorts by ItemCode)
        public int CompareTo(Item other)
        {
            return string.Compare(this.ItemCode, other.ItemCode, StringComparison.OrdinalIgnoreCase);
        }

        // Generic Sorting - Allows sorting by any property
        public static Func<Item, IComparable> GetSortingKey(string columnName)
        {
            return columnName switch
            {
                "Id" => item => item.Id,  // int implements IComparable
                "Item Name" => item => item.ItemName,  // string implements IComparable
                "Item Code" => item => item.ItemCode,  // string implements IComparable
                "Category" => item => item.Category,  // string implements IComparable
                "Quantity" => item => item.Quantity,  // int implements IComparable
                "Expiry Date" => item => DateTime.Parse(item.ExpiryDate),  // DateTime implements IComparable
                "Manufacture Date" => item => DateTime.Parse(item.ManufactureDate),  // DateTime implements IComparable
                "Gross Amount" => item => item.GrossAmount,  // double implements IComparable
                "Net Amount" => item => item.NetAmount,  // double implements IComparable
                _ => item => item.ItemCode // Default sorting by Item Code
            };
        }



        public override string ToString()
        {
            return $"{ItemCode} - {ItemName}";
        }
    }
}
