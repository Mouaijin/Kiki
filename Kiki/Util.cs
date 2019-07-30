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
            Claim claim          = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return new Guid(claim.Value);
        }
    }
}