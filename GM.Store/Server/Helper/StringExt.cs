
using System.Text;
using System.Text.RegularExpressions;
namespace GM.Store.Server.Helper
{
    public static class StringExt 
    {
        public static string CleanUrlString(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                //First to lower case
                value = value.ToLowerInvariant();

                //Remove all accents
                var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
                value = Encoding.ASCII.GetString(bytes);

                //Replace spaces
                value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

                //Remove invalid chars
                value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

                //Trim dashes from end
                value = value.Trim('-', '_');

                //Replace double occurences of - or _
                value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);
            }

            return value;
        }
        public static bool IsNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}
