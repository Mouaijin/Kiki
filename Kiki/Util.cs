using System.Text.Encodings.Web;
using System.Web;

namespace Kiki
{
    public static class Util
    {
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        public static string Remove(this string str, string substring)
        {
            return str.Replace(substring, "");
        }
    }
}