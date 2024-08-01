using MusicPortal.Models;
using MusicPortal.Repository;
using System.Security.Cryptography;

namespace MusicPortal.Services
{
   
    public class UserService : IUserRepository
    {
        public readonly MPContext _context;
        //HttpContext _httpContext;
       
        public UserService(MPContext context)
        {
            _context = context;
        }
        public string GenerateSalt(int nSalt)
        {
            var saltBytes = new byte[nSalt];
            using(var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public string HashPassword(string? password, string salt, int nIterations, int nHash)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using(var rfc = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
            {
                return Convert.ToBase64String(rfc.GetBytes(nHash));
            }
        }


        public async Task<User> CreateUser(User user, RegisterModel regModel)
        {
            await Task.Run(() =>
            {
                string salt = GenerateSalt(70);
                string? password = regModel.Password;
                string hashedPassword = HashPassword(password, salt, 10101, 70);
                string name = regModel.Name;
                string email = regModel.Email;

                user.Password = hashedPassword;
                user.Salt = salt;
                user.Name = name;
                user.Email = email;
                user.IsAdmin = true;
                user.IsConfirmed = true;
            });
            return user;
        }

        public async Task AddUserToDB(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public bool LogIn(AuthorizationModel authModel)
        {
            bool loginOk = false;
            string inputedEmail = authModel.Email;
            string inputedPassword = authModel.Password;

            User user1 = _context.Users.FirstOrDefault(user => user.Email == inputedEmail);
            if (user1!=null)
            {
                string hashedPassword = HashPassword(inputedPassword, user1.Salt, 10101, 70);
                if (user1.Email == inputedEmail && user1.Password == hashedPassword)
                {
                    loginOk = true;
                }

               
                //_httpContext.Session.SetString("Name", user1.Name ?? string.Empty);
                //_httpContext.Session.SetString("Email", user1.Email ?? string.Empty);
            }
            return loginOk;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = new List<User>();
            await Task.Run(() =>
            {
                foreach (var user in _context.Users)
                {
                    users.Add(user);
                }
            });          
            return users;
        }
    }
}
