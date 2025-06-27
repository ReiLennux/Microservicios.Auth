using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace ShoppingCart.API.Utility
{
    public class BackendApiAutenticationHttpClientHandler: DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;
        public BackendApiAutenticationHttpClientHandler(IHttpContextAccessor accessor)
        { 
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage>  SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _accessor.HttpContext?.Request?.Headers["Authorization"].ToString();
            Console.WriteLine($"TOKEN RAW: {token}");

            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
                Console.WriteLine($"TOKEN CLEAN: {token}");
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
