using LeoClinic.Application.Interfaces;
using BCrypt.Net;

namespace LeoClinic.Application.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            if (string.IsNullOrEmpty(passwordHash) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
