using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CookBook.Middelware
{
    public class AllowedCorsMiddelware
    {
        private readonly RequestDelegate _next;
        public AllowedCorsMiddelware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "*" });
            context.Response.Headers.Add("Access-Control-Allow-Origin-Methods", new[] { "*" });
            await _next(context);
        }
    }
}
