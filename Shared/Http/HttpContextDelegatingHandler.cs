using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Shared.DependencyInjection.Interfaces;

namespace Shared.Http;

public class HttpContextDelegatingHandler : DelegatingHandler, ITransient
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext != null && httpContext.Request.Headers.ContainsKey("Authorization"))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", httpContext.Request.Headers["Authorization"].ToString());
        }

        return await base.SendAsync(request, cancellationToken);
    }
}