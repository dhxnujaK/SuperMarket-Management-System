using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA_SuperMarket_Management_System
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string ContactNumber { get; set; }

        public User(int id, string name, string nic, string contactNumber)
        {
            Id = id;
            Name = name;
            NIC = nic;
            ContactNumber = contactNumber;
        }
    }

}
