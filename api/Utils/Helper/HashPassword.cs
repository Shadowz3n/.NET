using System.Web.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace API.Utils.Helper
{
    /// <summary>
    /// Hash password.
    /// </summary>
    public class HashPassword
    {
        private static string _salt = WebConfigurationManager.AppSettings["HashSalt"];

        public string Generate(string password)
        {
            string crypt = ComputeSha256Hash(_salt + ComputeSha256Hash(_salt + ComputeSha256Hash(password + _salt))).ToLower();

            return crypt;
        }

        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
