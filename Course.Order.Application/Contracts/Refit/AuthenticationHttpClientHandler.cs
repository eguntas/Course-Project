using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Order.Application.Contracts.Refit
{
    internal class AuthenticationHttpClientHandler(IHttpContextAccessor httpContextAccessor):DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(httpContextAccessor.HttpContext is null)
                return await base.SendAsync(request, cancellationToken);

            if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                return await base.SendAsync(request, cancellationToken);
            
            string? token = null;

            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                token = authHeader.ToString().Replace("Bearer ", "");
            }

            if(!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            return await base.SendAsync(request, cancellationToken);

        }
    }
}
