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

        public async Task<ContentCreateResponse> Create(ContentCreateRequest contentCreateRequest)
        {
            var baseApiResult = await PostRequest<ContentCreateResponse>("Content/Create", contentCreateRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<ContentUpdateResponse> Update(ContentUpdateRequest contentUpdateRequest)
        {
            var baseApiResult =
                await PutRequest<ContentUpdateResponse>($"Content/Update/{contentUpdateRequest.Content.Id}",
                                                        contentUpdateRequest);

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<List<Content>> List()
        {
            var baseApiResult = await GetRequest<ContentListResponse>("Content/List");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return ((ContentListResponse) baseApiResult.Response).Contents;
        }

        public async Task<Content> Read(string id)
        {
            var baseApiResult = await GetRequest<ContentReadResponse>($"Content/Read/{id}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return ((ContentReadResponse) baseApiResult.Response).Content;
        }

        public async Task<ContentDeleteResponse> Delete(string id)
        {
            var baseApiResult = await DeleteRequest<ContentDeleteResponse>($"Content/Delete/{id}");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return baseApiResult.Response;
        }

        public async Task<List<Content>> PublicList()
        {
            var baseApiResult = await GetRequest<ContentListResponse>("Content/Public/List");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return ((ContentListResponse) baseApiResult.Response).Contents;
        }

        public async Task<Content> PublicRead(string urlFriendlyTitle)
        {
            var baseApiResult = await GetRequest<ContentReadResponse>("Content/Public/Read");

            if (!baseApiResult.Success)
                throw new Exception(baseApiResult.Message);

            return ((ContentReadResponse) baseApiResult.Response).Content;
        }

    }
}