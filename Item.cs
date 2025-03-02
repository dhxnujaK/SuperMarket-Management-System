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

        public int CompareTo(Item other)
        {
            return string.Compare(this.ItemCode, other.ItemCode, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"{ItemCode} - {ItemName}";
        }
    }

}

