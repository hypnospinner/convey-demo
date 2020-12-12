using System;
using Microsoft.AspNetCore.Http;

namespace UnicornParty.Utils
{
    public static class HttpContextHelper
    {
        public static Guid GetUserId(this HttpContext context)
        {
            if (context.User == null || context.User.Identity == null)
                throw new Exception("No user");

            return new Guid(context.User.Identity.Name);
        }
    }
}