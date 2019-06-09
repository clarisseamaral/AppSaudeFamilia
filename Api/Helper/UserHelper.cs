using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ColetaApi.Helper
{
    public static class UserHelper
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            var claimValue = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(claimValue);
        }
    }
}
