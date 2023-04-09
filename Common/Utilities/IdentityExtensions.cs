using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public static class IdentityExtensions
    {
        public static string FindFirstValue(this ClaimsIdentity claimsIdentity,string claimType)
        {
            return claimsIdentity?.FindFirst(claimType)?.Value;
        }

        public static string FindFirstValue(this IIdentity identity,string claimType)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity?.FindFirstValue(claimType);
        }

    }
}
