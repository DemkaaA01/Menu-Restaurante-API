using System.Text;
using System.Security.Cryptography;

namespace Menu_Restaurante_API.Helpers
{
    public static class PasswordHelper
    {
        // Genera un hash SHA256 en Base64
        public static string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Compara un password plano con un hash guardado
        public static bool Verify(string plainPassword, string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
                return false;

            var hashOfInput = Hash(plainPassword);
            return hashOfInput == hashedPassword;
        }
    }
}
