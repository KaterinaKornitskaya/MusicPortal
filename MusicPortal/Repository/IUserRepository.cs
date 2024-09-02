using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public interface IUserRepository
    {
        string GenerateSalt(int nSalt);
        string HashPassword(string password, string salt, int nIterations, int nHash);
        Task<User> CreateUser(User user, RegisterModel model);
        bool LogIn(AuthorizationModel authModel);
        Task AddUserToDB(User user);
        IEnumerable<User> GetAllUsers();         
        User GetUserById(int id);
        Task ConfirmUser(int id);
        User GetUserByEmail(string email);
        Task DeleteById(int id);
    }
}
