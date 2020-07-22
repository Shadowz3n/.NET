using System.Net.Mail;

namespace API.Utils
{
    public static class EmailValidation
    {
        public static bool Email(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}