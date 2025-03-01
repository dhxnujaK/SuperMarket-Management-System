using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class Item : IComparable<Item>
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string Category { get; set; }
        public string ExpiryDate { get; set; }
        public string ManufactureDate { get; set; }
        public double GrossAmount { get; set; }
        public double NetAmount { get; set; }
        public int Quantity { get; set; }

        // Implement IComparable interface to allow comparison of Item objects
        public int CompareTo(Item other)
        {
            if (other == null) return 1;

            // Comparing based on the Id field (you can modify this to use other fields)
            return this.Id.CompareTo(other.Id);
        }
    }
}
