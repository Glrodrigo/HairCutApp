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

        public static bool IsHexColor(string hexColor)
        {
            string pattern = @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";

            return Regex.IsMatch(hexColor, pattern);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
