using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                user.IsAdmin = false;
                user.IsConfirmed = false;
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

        public IEnumerable<User> GetAllUsers()
        {
            //var userList = new List<User>();
            //Task.Run(() =>
            //{
            //    userList = _context.Users.Include(e => e.Songs).ToList();

            //});
            //return userList;
            var userList = _context.Users.Include(e => e.Songs).ToList();
            return userList;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.Include(e=>e.Songs).FirstOrDefault(e=>e.Id == id);
            return user;
        }

        public async Task ConfirmUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                user.IsConfirmed = true;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.Include(e=>e.Songs).FirstOrDefault(e=>e.Email==email);
            return user;
        }

        public async Task DeleteById(int id)
        {
            var user = GetUserById(id);
            if (user != null) 
            {
                _context.Users.Remove(user);
            }
            await _context.SaveChangesAsync();
        }
    }
}
