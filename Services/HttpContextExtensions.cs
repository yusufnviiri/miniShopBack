using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class HttpContextExtensions
    {
        public static string UserAgent(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext?
                .Request.Headers["User-Agent"]
                .ToString() ?? "unknown";
        }

        public static string IpAddress(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext?
                .Connection.RemoteIpAddress?
                .ToString() ?? "unknown";
        }
    }

}
