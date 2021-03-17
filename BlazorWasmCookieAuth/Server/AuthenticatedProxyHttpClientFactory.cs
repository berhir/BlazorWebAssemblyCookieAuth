using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.ReverseProxy.Service.Proxy.Infrastructure;
using System.Net;
using System.Net.Http;

namespace BlazorWasmCookieAuth.Server
{
    public class AuthenticatedProxyHttpClientFactory : IProxyHttpClientFactory
    {
        private readonly UserAccessTokenHandler _userAccessTokenHandler;

        public AuthenticatedProxyHttpClientFactory(UserAccessTokenHandler userAccessTokenHandler)
        {
            _userAccessTokenHandler = userAccessTokenHandler;

            var socketsHttpHandler = new SocketsHttpHandler
            {
                UseProxy = false,
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.None,
                UseCookies = false
            };

            _userAccessTokenHandler.InnerHandler = socketsHttpHandler;
        }

        public HttpMessageInvoker CreateClient(ProxyHttpClientContext context)
        {
            if (context.OldClient != null && context.NewOptions == context.OldOptions)
            {
                return context.OldClient;
            }

            return new HttpMessageInvoker(_userAccessTokenHandler, disposeHandler: false);
        }
    }
}
