using System;
using System.Text.RegularExpressions;
using API.Models;

namespace API.Utils.Validation
{
    public class PassValidation
    {
        public string Pass(UserRegister user)
        {
            /* Match passwords */
            if (user.Password != user.CheckPassword)
                return "error.validation.incorrectPasswordCheck";

            /* Check password string */
            Regex passRegex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            Match passMatch = passRegex.Match(user.Password);
            if (!passMatch.Success)
                return "error.validation.incorrectPasswordRegex";

            /* Check if phone is password  */
            if (user.Password.ToLower().IndexOf(user.Phone.ToLower().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""), StringComparison.Ordinal) >= 0)
                return "error.validation.passwordEqualsPhone";

            /* Check if name is password */
            if (user.Password.ToLower().IndexOf(user.Name.ToLower(), StringComparison.Ordinal) >= 0 || user.Password.ToLower().IndexOf(user.Lastname.ToLower(), StringComparison.Ordinal) >= 0)
                return "error.validation.passwordEqualsName";

            return "OK";
        }
    }
}
