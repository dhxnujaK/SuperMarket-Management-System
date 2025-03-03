using System;

public class User : IComparable<User>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NIC { get; set; }
    public string ContactNumber { get; set; }

    // Parameterless constructor (needed for some generic handling)
    public User() { }

    public User(int id, string name, string nic, string contactNumber)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name)); // Ensure name is not null
        NIC = nic ?? throw new ArgumentNullException(nameof(nic)); // Ensure NIC is not null
        ContactNumber = contactNumber ?? string.Empty; // Default empty string for safety
    }

    // Implement IComparable<User> for sorting in BinarySearchTree
    public int CompareTo(User other)
    {
        if (other == null) return 1;

        return this.NIC.CompareTo(other.NIC); // Sort based on NIC instead of ID
    }

    // Override ToString for debugging
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, NIC: {NIC}, Contact: {ContactNumber}";
    }
}
