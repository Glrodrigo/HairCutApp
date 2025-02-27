using System.Text.RegularExpressions;

namespace HairCut.Generals
{
    public class StringFormat
    {
        public static bool isEmail(string email)
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            return Regex.IsMatch(email, pattern);
        }

        public static bool isValidPassword(string password)
        {
            string pattern = @"(?=(?:.*[a-zA-Z]){5,})(?=.*[A-Z])(?=.*[\W_]).+$";

            return Regex.IsMatch(password, pattern);
        }
    }
}
