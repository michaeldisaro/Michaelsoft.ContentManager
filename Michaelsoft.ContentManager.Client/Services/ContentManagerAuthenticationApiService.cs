using System;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Settings;
using Michaelsoft.ContentManager.Common.HttpModels.Authentication;
using Microsoft.AspNetCore.Http;

namespace Michaelsoft.ContentManager.Client.Services
{
    public class ContentManagerAuthenticationApiService : ContentManagerBaseApiService,
                                                          IContentManagerAuthenticationApiService
    {

        public ContentManagerAuthenticationApiService(IContentManagerClientSettings settings,
                                                      IHttpClientFactory httpClientFactory,
                                                      IHttpContextAccessor httpContextAccessor) :
            base(settings, httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<TokenResponse> RequestToken(string author)
        {
            var tokenRequest = new TokenRequest
            {
                Author = author
            };

            var baseApiResult = await PostRequest<TokenResponse>("Token", tokenRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<AuthorizeResponse> Authorize(string encryptedToken)
        {
            var authorizeRequest = new AuthorizeRequest
            {
                EncryptedToken = encryptedToken
            };

            var baseApiResult = await PostRequest<AuthorizeResponse>("Authorize", authorizeRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<LogoutResponse> Logout()
        {
            var baseApiResult = await PostRequest<LogoutResponse>("Logout");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

    }
}