using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string Category { get; set; }
        public string ExpiryDate { get; set; }
        public string ManufactureDate { get; set; }
        public double GrossAmount { get; set; }
        public double NetAmount { get; set; }
        public int Quantity { get; set; }
    }
}
