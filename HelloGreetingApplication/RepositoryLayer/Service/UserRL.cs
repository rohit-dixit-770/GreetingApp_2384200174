using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Middleware.JWT;
using Middleware.PasswordHelper;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly GreetingContext _dbContext;
        private readonly ILogger _logger;
        public UserRL(GreetingContext dbContext, ILogger<UserRL> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user with hashed password
        /// </summary>
        public RegistrationResponse RegisterUser(RegisterModel register)
        {
            var existingUser = _dbContext.Users.FirstOrDefault<UserEntity>(e => e.Email == register.Email);

            if (existingUser == null)
            {
                // Generate Salt
                var salt = HashingPassword.GenerateSalt();

                // Hash Password with Salt
                string hashedPassword = HashingPassword.HashPassword(register.Password, salt);

                var newUser = new UserEntity
                {
                    Name = register.Name,
                    Email = register.Email,
                    Password = hashedPassword,
                    Salt = salt 
                };

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();
                return new RegistrationResponse { UserId = newUser.UserId, Name = newUser.Name, Email = newUser.Email };
            }

            return new RegistrationResponse { UserId = existingUser.UserId, Name = existingUser.Name, Email = existingUser.Email };
        }


        /// <summary>
        /// Authenticates user by verifying password hash
        /// </summary>
        public string LoginUser(LoginModel login)
        {
            var existingUser = _dbContext.Users.FirstOrDefault<UserEntity>(e => e.Email == login.Email);

            if (existingUser == null)
            {
                return "User not found";
            }

            // Verify hashed password using the stored salt
            bool isPasswordValid = HashingPassword.VerifyPassword(login.Password, existingUser.Password, existingUser.Salt);

            if (!isPasswordValid)
            {
                return "Invalid credentials";
            }

            return "Login successful";
        }


        public UserEntity GetUser(string email)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email == email);

            return existingUser;
        }

        public void UpdateUser(UserEntity user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            existingUser.Email = user.Email;
            existingUser.Name = user.Name;
        }
    }
}
