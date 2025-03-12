using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.PasswordHelper
{
    public class HashingPassword
    {
        private const int SaltSize = 16;
        private const int HashSize = 32; 
        private const int Iterations = 10000; 

        /// <summary>
        /// Hashes the password using PBKDF2 with salt
        /// </summary>
        public static string HashPassword(string userPass , byte[] salt)
        {

            var pbkdf2 = new Rfc2898DeriveBytes(userPass, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

                byte[] hashByte = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashByte, 0, SaltSize);
                Array.Copy(hash, 0, hashByte, SaltSize, HashSize);

                string hashedPassword = Convert.ToBase64String(hashByte);

                return hashedPassword;
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];            
            new RNGCryptoServiceProvider().GetBytes(salt);

            return salt;
        }

        /// <summary>
        /// Verifies a password against a stored hashed password
        /// </summary>
        public static bool VerifyPassword(string enteredPass, string storedHash, byte[] salt)
        {
            byte[] hashByte = Convert.FromBase64String(storedHash);

            byte[] computedHash = new Rfc2898DeriveBytes(enteredPass, salt, Iterations).GetBytes(HashSize);

            // Compare stored hash with computed hash
            for (int i = 0; i < HashSize; i++)
            {
                if (hashByte[i + SaltSize] != computedHash[i])
                    return false;
            }

            return true;
        }

    }
}
