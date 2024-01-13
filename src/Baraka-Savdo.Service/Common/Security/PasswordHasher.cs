using Org.BouncyCastle.Crypto.Generators;

using System.Security.Cryptography;
using System.Text;

namespace Baraka_Savdo.Service.Common.Security;

public class PasswordHasher
{
    // Parolni hashlash uchun funktsiya
    public static string HashPassword(string password, string salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Parolni baytga o'girish
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

            // Baytlarni stringga aylantirish
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }

    // Parolni tekshirish uchun funktsiya
    public static bool VerifyPassword(string inputPassword, string salt, string hashedPassword)
    {
        // Hashlash vaqtida bitta kiritilgan parolni tekshirish
        string inputHashed = HashPassword(inputPassword, salt);

        // Hashlanmagan parolni saqlangan hash bilan solishtirish
        return string.Equals(inputHashed, hashedPassword, StringComparison.OrdinalIgnoreCase);
    }
}


