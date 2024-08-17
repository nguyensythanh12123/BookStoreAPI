namespace BookStoreAPI.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //if (!context.Request.Headers.ContainsKey("xAuth") || string.IsNullOrWhiteSpace(context.Request.Headers["xAuth"]))
            //{
            //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    return;
            //}

            await _next(context);
        }
    }
}
