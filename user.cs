using System;

public class User : IComparable<User>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NIC { get; set; }
    public string ContactNumber { get; set; }

    
    public User() { }

    public User(int id, string name, string nic, string contactNumber)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name)); 
        NIC = nic ?? throw new ArgumentNullException(nameof(nic)); 
        ContactNumber = contactNumber ?? string.Empty; 
    }

    // Implement IComparable<User> for sorting in BinarySearchTree
    public int CompareTo(User other)
    {
        if (other == null) return 1;

        return this.NIC.CompareTo(other.NIC); // Sort based on NIC instead of ID
    }

   
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, NIC: {NIC}, Contact: {ContactNumber}";
    }
}
