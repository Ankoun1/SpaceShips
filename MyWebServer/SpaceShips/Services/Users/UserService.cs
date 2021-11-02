namespace SpaceShips.Services.Users
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Data;
    using Data.Models;
    using Models.Administrator;
    using Services.Validators;

    public class UserService : IUserService
    {
        private readonly SpaceShipsDbContext data;       

        public UserService(SpaceShipsDbContext data)
        {
            this.data = data;            
        }

        public bool AdministratorExists()
        => data.Users.Any();

        public void CreateAdministrator(string username,string password)
        {
            var user = new User
            {
                Username = username,
                Password = HashPassword(password)
            };

            data.Users.Add(user);

            data.SaveChanges();
        }

        public string GetUserId(string username, string password)
         => this.data
               .Users
               .Where(u => u.Username == username && u.Password == HashPassword(password))
               .Select(u => u.Id)
               .FirstOrDefault();

        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }

            // Create a SHA256   
            using var sha256Hash = SHA256.Create();

            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string   
            var builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
