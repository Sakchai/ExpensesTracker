
using Expenses.DB;
using Microsoft.AspNetCore.Identity;

namespace Expenses.Core
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        // Method to hash a password
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        // Method to verify a password
        public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        }
    }
}
