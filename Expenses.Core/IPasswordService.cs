using Microsoft.AspNetCore.Identity;

namespace Expenses.Core
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword);
    }
}