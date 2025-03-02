using System;

public class User : IComparable<User>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NIC { get; set; }
    public string ContactNumber { get; set; }

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

        return this.Id.CompareTo(other.Id); // Sort based on ID
        // If sorting by NIC instead, use: return this.NIC.CompareTo(other.NIC);
    }
}
