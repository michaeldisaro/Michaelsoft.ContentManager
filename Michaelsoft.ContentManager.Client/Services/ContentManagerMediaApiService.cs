using System;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Settings;
using Michaelsoft.ContentManager.Common.HttpModels.Media;
using Microsoft.AspNetCore.Http;

namespace Michaelsoft.ContentManager.Client.Services
{
    public class ContentManagerMediaApiService : ContentManagerBaseApiService,
                                                 IContentManagerMediaApiService
    {

        public ContentManagerMediaApiService(IContentManagerClientSettings settings,
                                             IHttpClientFactory httpClientFactory,
                                             IHttpContextAccessor httpContextAccessor) :
            base(settings, httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<MediaCreateResponse> Create(MediaCreateRequest mediaCreateRequest)
        {
            var baseApiResult = await PostRequest<MediaCreateResponse>("/Media/Create", mediaCreateRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<byte[]> Read(string filenameWithExtension)
        {
            var baseApiResult = await GetRequest<MediaReadResponse>($"/Media/Read/{filenameWithExtension}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return ((MediaReadResponse) baseApiResult.Response).Content;
        }

    }
}