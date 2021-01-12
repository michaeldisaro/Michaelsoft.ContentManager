using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Settings;
using Michaelsoft.ContentManager.Common.HttpModels.Authentication;
using Michaelsoft.ContentManager.Common.HttpModels.Content;
using Microsoft.AspNetCore.Http;

namespace Michaelsoft.ContentManager.Client.Services
{
    public class ContentManagerContentApiService : ContentManagerBaseApiService,
                                                   IContentManagerContentApiService
    {

        public ContentManagerContentApiService(IContentManagerClientSettings settings,
                                               IHttpClientFactory httpClientFactory,
                                               IHttpContextAccessor httpContextAccessor) :
            base(settings, httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<CreateResponse> CreateContent(CreateRequest createRequest)
        {
            var baseApiResult = await PostRequest<CreateResponse>("Create", createRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<UpdateResponse> UpdateContent(UpdateRequest updateRequest)
        {
            var baseApiResult = await PutRequest<UpdateResponse>($"Update/{updateRequest.Content.Id}", updateRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<List<Content>> ListContents()
        {
            var baseApiResult = await GetRequest<ListResponse>("List");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return ((ListResponse) baseApiResult.Response).Contents;
        }

        public async Task<Content> ReadContent(string contentId)
        {
            var baseApiResult = await GetRequest<ReadResponse>($"Read/{contentId}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return ((ReadResponse) baseApiResult.Response).Content;
        }

        public async Task<List<Content>> PublicListContents()
        {
            var baseApiResult = await GetRequest<ListResponse>("PublicList");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return ((ListResponse) baseApiResult.Response).Contents;
        }

        public async Task<Content> PublicReadContent(string urlFriendlyTitle)
        {
            var baseApiResult = await GetRequest<ReadResponse>("PublicRead");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return ((ReadResponse) baseApiResult.Response).Content;
        }

    }
}