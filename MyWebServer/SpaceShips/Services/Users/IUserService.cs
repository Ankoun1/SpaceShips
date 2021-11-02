namespace SpaceShips.Services.Users
{ 
    public interface IUserService
    {
        string HashPassword(string password);

        void CreateAdministrator(string username,string password);

        string GetUserId(string username, string password);

        bool AdministratorExists();
    }
}
