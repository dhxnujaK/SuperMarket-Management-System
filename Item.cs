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

        // Custom Sorting - Sort by any property dynamically
        public static Comparison<Item> GetComparison(string columnName)
        {
            return columnName switch
            {
                "Id" => (x, y) => x.Id.CompareTo(y.Id),
                "Item Name" => (x, y) => string.Compare(x.ItemName, y.ItemName, StringComparison.OrdinalIgnoreCase),
                "Item Code" => (x, y) => string.Compare(x.ItemCode, y.ItemCode, StringComparison.OrdinalIgnoreCase),
                "Quantity" => (x, y) => x.Quantity.CompareTo(y.Quantity),
                "Expiry Date" => (x, y) => DateTime.Parse(x.ExpiryDate).CompareTo(DateTime.Parse(y.ExpiryDate)),
                "Manufacture Date" => (x, y) => DateTime.Parse(x.ManufactureDate).CompareTo(DateTime.Parse(y.ManufactureDate)),
                "Gross Amount" => (x, y) => x.GrossAmount.CompareTo(y.GrossAmount),
                "Net Amount" => (x, y) => x.NetAmount.CompareTo(y.NetAmount),
                _ => (x, y) => 0 // Default to no sorting if invalid column
            };
        }

        public override string ToString()
        {
            return $"{ItemCode} - {ItemName}";
        }
    }
}
