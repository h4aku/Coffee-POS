
namespace BisleriumCafe.Data
{
    // Represents a User entity
    public class User
    {
        // Generate a new GUID for each new User object
        public Guid UserID { get; set; } = Guid.NewGuid();

        // The username of the user
        public string Username { get; set; }

        // The hashed password of the user
        public string PasswordHash { get; set; }

        // The role of the user (e.g. admin, customer, etc.)
        public Role Role { get; set; }
    }
}