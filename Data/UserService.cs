using System.Text.Json;

namespace BisleriumCafe.Data
{
    public class UserService
    {
        public const string seedUsername = "admin";
        public const string seedPassword = "admin";

        // Save the user information to a JSON file
        private static void SaveUserInfo(List<User> users)
        {
            string appDataPath = Utils.GetAppDirectory();
            string userFilePath = Utils.GetUsersPath();

            // Create the app data directory if it doesn't exist
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            // Serialize the users list to JSON format
            var json = JsonSerializer.Serialize(users);

            // Write the JSON data to the user file
            File.WriteAllText(userFilePath, json);
        }

        // Retrieve the user information from the JSON file
        public static List<User> GetUserInfo()
        {
            string userFilePath = Utils.GetUsersPath();

            // If the user file does not exist, return an empty list
            if (!File.Exists(userFilePath))
            {
                return new List<User>();
            }

            // Read the JSON data from the user file
            var json = File.ReadAllText(userFilePath);

            // Deserialize the JSON data to a list of users
            var users = JsonSerializer.Deserialize<List<User>>(json);

            return users;
        }

        // Create a new user with the given information
        public static List<User> CreateNewUser(Guid userId, string username, string password, Role role)
        {
            List<User> users = GetUserInfo();

            // Check if the username already exists
            bool usernameExist = users.Any(x => x.Username == username);
            if (usernameExist)
            {
                throw new Exception("This username is already taken!");
            }

            // Create a new user object and add it to the users list
            users.Add(new User
            {
                Username = username,
                PasswordHash = Utils.HashSecretKey(password),
                Role = role
            });

            // Save the updated user information
            SaveUserInfo(users);

            return users;
        }

        // Seed default users if no admin user exists
        public static void SeedDefaultUsers()
        {
            // Check if any admin users exist
            var users = GetUserInfo().FirstOrDefault(x => x.Role == Role.Admin);

            if (users == null)
            {
                // Create a new admin user
                CreateNewUser(Guid.Empty, seedUsername, seedPassword, Role.Admin);
            }
        }

        // Delete an existing user with the given ID
        public static List<User> DeleteExistingUser(Guid ID)
        {
            List<User> users = GetUserInfo();

            // Find the user with the given ID
            User user = users.FirstOrDefault(x => x.UserID == ID);

            // If no user is found, throw an exception
            if (user == null)
            {
                throw new Exception("User does not exist!");
            }

            // Remove the user from the users list
            users.Remove(user);

            // Save the updated user information
            SaveUserInfo(users);

            return users;
        }

        // Validate the login credentials and return the user object
        public static User Login(string username, string password)
        {
            List<User> users = GetUserInfo();

            // Find the user with the given username
            User user = users.FirstOrDefault(x => x.Username == username);

            // If no user is found, throw an exception
            if (user == null)
            {
                throw new Exception("Invalid credentials!");
            }

            // Verify the password
            bool pwMatched = Utils.HashVerification(password, user.PasswordHash);

            // If the password does not match, throw an exception
            if (!pwMatched)
            {
                throw new Exception("Invalid credentials!");
            }

            return user;
        }
    }
}