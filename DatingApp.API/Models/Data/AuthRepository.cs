using System;
using System.Security.AccessControl;
using DatingApp.API.Models.Data;
using System.Threading.Tasks;
using DatingApp.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Data;
using DatingApp.API.Models;


namespace DatingApp.API.Models.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        { 
            _context = context;
            
        }

        public async Task<User> Register(User user, string password){
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.Salt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();  
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if(user == null)
                return null;
            if(!VerifyPasswordHash(password, user.PasswordHash, user.Salt))
                return null;
            
            return user;
            
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }

            }
                return true;
        }
        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passWordSalt){
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passWordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;
            return false;
            
        }
    }
}