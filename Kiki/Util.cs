using System;
using System.Security.Claims;
using System.Security.Principal;
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

        public static Guid GetUserId(this IPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity) principal.Identity;
            Claim          claim          = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return new Guid(claim.Value);
        }

        /// <summary>
        /// Get the the longest common substring
        /// Shamelessly stolen from rosetta code
        /// </summary>
        public static string LCS(this string a, string b)
        {
            var    lengths        = new int[a.Length, b.Length];
            int    greatestLength = 0;
            string output         = "";
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    if (a[i] == b[j])
                    {
                        lengths[i, j] = i == 0 || j == 0 ? 1 : lengths[i - 1, j - 1] + 1;
                        if (lengths[i, j] > greatestLength)
                        {
                            greatestLength = lengths[i, j];
                            output         = a.Substring(i - greatestLength + 1, greatestLength);
                        }
                    }
                    else
                    {
                        lengths[i, j] = 0;
                    }
                }
            }

            return output;
        }
    }
}